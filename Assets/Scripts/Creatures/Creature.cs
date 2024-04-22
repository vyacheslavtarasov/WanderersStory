using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Creature : MonoBehaviour
{
    [Header("Horizontal Movement Adjustments")]
    [SerializeField] protected float _runAccelerationMultiplier = 0.1f;
    [SerializeField] protected float _maximumHorizontalVelocity = 4.5f;
    [SerializeField] protected float _runBrakingMultiplier = 0.8f;

    [SerializeField] protected float _runAccelerationMultiplierSlope = 0.5f;
    [SerializeField] protected float _maximumHorizontalVelocitySlopeAscend = 4.5f;
    [SerializeField] protected float _maximumHorizontalVelocitySlopeDescend = 4.5f;
    [SerializeField] protected float _runBrakingMultiplierSlope = 0.1f;

    [Space]
    [SerializeField] protected float _attackDamage = -1.0f;
    [SerializeField] protected float _damageForceToInflict = 200.0f;

    // Necessary information
    public Vector3 _direction;
    private bool attackMode = false;
    protected bool _isGrounded = false; // if the hero is on the ground
    protected bool _priorGrounded = false;
    protected bool _runParticleAvailable = false;
    protected bool _isJumping = false; // if the jump pressed
    protected int _currentJumpsCount = 0;
    protected bool _jumpAvailable = true; // we need to avoid secondary jumps right after landing when jump button is kept pressed
    protected float _fallTime = 0.0f;
    protected float _ascendTime = 0.0f;

    [Space]
    [Header("Jump Adjustments")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _minimalAscendTime = 1.0f;
    [SerializeField] protected int _jumpsAmount = 2;

    [Space]
    [Header("Sensors")]
    [SerializeField] protected Sensor _attackSensor;
    [SerializeField] protected Sensor GroundChecker;

    [SerializeField] private SpawnPrefab ProjectileSpawner;
    [SerializeField] private Cooldown _projectileSpawnCooldown;
     

    protected Animator _animator;
    protected Rigidbody2D _rigidbody;
    protected Health _healthComponent;
    protected Inventory _inventory;
    [SerializeField] protected SoundPlayer _soundPlayer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _healthComponent = GetComponent<Health>();
        _inventory = GetComponent<Inventory>();
    }

    public void SetDirection(Vector3 newDirection)
    {

        _direction = newDirection;
    }

    public void SetJumping(bool isJumping)
    {
        _isJumping = isJumping;
        if (_isJumping)
        {
            _soundPlayer.Play("jump");
        }
        
    }

    public void Attack()
    {
        attackMode = true;
        if (_attackSensor != null)
        {
            _animator.SetTrigger("attack");
        }
        
    }

    public void DoAttack()
    {
        foreach (GameObject obj in _attackSensor.GetIntersectedObjects().ToArray())
        {
            Health healthComponennt = obj.GetComponent<Health>();
            healthComponennt.ChangeHealth(_attackDamage);
        }
        attackMode = false;
    }

    public void SpellCast()
    {
        if (!_projectileSpawnCooldown.IsReady()) 
        {
            return;
        }
        _projectileSpawnCooldown.Reset();
        attackMode = true;
        _animator.SetTrigger("spellCast");
    }

    public void DoSpellCast()
    {
        ProjectileSpawner.Spawn();
    }

    public void OnSpellCastEnd()
    {
        attackMode = false;
    }
    protected void CheckIsGrounded()
    {
        _isGrounded = GroundChecker.GetCollisionStatus();
    }
    public virtual void OnChangeHealth(float wasHealth, float currentHealth, float overallHealth)
    {
        if (wasHealth > currentHealth)
        {
            _animator.SetTrigger("hit");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _damageForceToInflict, ForceMode2D.Force);
        }
    }

    protected void FlipSprite()
    {
        if (_direction.x < 0)
        {
            // _spriteRenderer.flipX = true;
            // doing it via transform for dust paritcles position modification
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            // _spriteRenderer.flipX = false;
        }
    }

    protected void SetAnimationParameters()
    {

        if (_direction.x != 0.0f) // button is pressed
        {
            _animator.SetBool("isRunning", true);
        }
        else
        {
            _animator.SetBool("isRunning", false);
        }

        _animator.SetFloat("verticalVelocity", _rigidbody.velocity.y);

        if (_isGrounded) // button is pressed
        {
            _animator.SetBool("isGrounded", true);
        }
        else
        {
            _animator.SetBool("isGrounded", false);
        }
        if (_isJumping) // button is pressed
        {
            _animator.SetBool("isJumping", true);
        }
        else
        {
            _animator.SetBool("isJumping", false);
        }

        _animator.SetFloat("fallTime", _fallTime);
        _animator.SetFloat("ascendTime", _ascendTime);
    }

    protected virtual void FixedUpdate()
    {
        CheckIsGrounded(); // Now _airborne variable always show if the carrier is grounded.

        if (_rigidbody.velocity.y < 0.1f && !_isGrounded)
        {
            _rigidbody.gravityScale = 3.5f;
        }
        else
        {
            _rigidbody.gravityScale = 2.0f;
        }

        if (_rigidbody.velocity.y < -0.01f)
        {
            _fallTime += 0.02f;
        }

        if (_rigidbody.velocity.y > 0.01f)
        {
            _ascendTime += 0.02f;
        }

        // Became airborne event
        // It is here this way because catching it in different compoent
        // leads to uneven results. (fixed update for different objects can be called in different order)
        if (_isGrounded == false && _priorGrounded == true)
        {
            _ascendTime = 0.0f;
            _fallTime = 0.0f;

            // became airborne without jumping is basically fall
            if (!_isJumping)
            {
                // we need to do it to avoid inertia at the end of the slope.
                if (_rigidbody.velocity.y > 0)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
                }
                else
                {
                    _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
                }
            }
        }

        // Horizontal movement.
        // Parameters depends on whether Hero is on slope or plane
        // _rigidbody.velocity = new Vector2(_direction.x * 3.0f, _rigidbody.velocity.y );

        float accelerating = _runAccelerationMultiplier;
        float breaking = _runBrakingMultiplier;
        float velocityLimit = _maximumHorizontalVelocity;

        bool onPlane;
        bool ascendSlope = false;

        float xVelocityAbs = Mathf.Abs(_rigidbody.velocity.x);
        float yVelocityAbs = Mathf.Abs(_rigidbody.velocity.y);

        if (xVelocityAbs > 0.02f && yVelocityAbs > 0.02f && _isGrounded)
        {
            onPlane = false;
            ascendSlope = _rigidbody.velocity.y > 0;
        }
        else
        {
            onPlane = true;
        }

        if (onPlane)
        {
            accelerating = _runAccelerationMultiplier;
            breaking = _runBrakingMultiplier;
            velocityLimit = _maximumHorizontalVelocity;
        }
        else
        {
            accelerating = _runAccelerationMultiplierSlope;
            breaking = _runBrakingMultiplierSlope;
            velocityLimit = ascendSlope ? _maximumHorizontalVelocitySlopeAscend : _maximumHorizontalVelocitySlopeDescend;
        }

        Vector2 newVelocity = Vector2.zero;

        if (_direction.x >= 0.01f)
        {
            newVelocity = new Vector2(_direction.x * (velocityLimit - _rigidbody.velocity.x) * accelerating + _rigidbody.velocity.x, _rigidbody.velocity.y);

            if (Mathf.Abs(velocityLimit - _rigidbody.velocity.x) < 0.1f)
            {
                newVelocity = new Vector2(velocityLimit, _rigidbody.velocity.y);
            }
        }

        if (_direction.x <= -0.01f)
        {
            newVelocity = new Vector2(-_direction.x * (-velocityLimit - _rigidbody.velocity.x) * accelerating + _rigidbody.velocity.x, _rigidbody.velocity.y);

            if (Mathf.Abs(velocityLimit + _rigidbody.velocity.x) < 0.1f)
            {
                newVelocity = new Vector2(-velocityLimit, _rigidbody.velocity.y);
            }
        }


        if (_direction.x < 0.01f && _direction.x > -0.01f && _isGrounded)
        {
            if (xVelocityAbs > 0.1f)
            {
                newVelocity = new Vector2(_rigidbody.velocity.x, 0) * breaking;
            }
            else
            {
                newVelocity = Vector2.zero;
            }
        }
        // Avoiding slipping from slipper slopes
        if (newVelocity.magnitude < 0.1f && _isGrounded && !_isJumping)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
        }
        else
        {
            _rigidbody.velocity = new Vector2(newVelocity.x, _rigidbody.velocity.y);
        }

        if (attackMode)
        {
            _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
        }

        // Jumping
        // The firt jump must alway be from the ground.
        if (_isJumping && _jumpAvailable && _isGrounded)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            _currentJumpsCount = _jumpsAmount - 1;
            _jumpAvailable = false;
            OnJump();
        }

        if (_isJumping && _currentJumpsCount > 0 && _jumpAvailable && !_isGrounded)
        {
            _rigidbody.velocity = new Vector2(this._rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            _currentJumpsCount -= 1;
            _jumpAvailable = false;

            _ascendTime = 0.0f;
            _fallTime = 0.0f;
            OnJump();
        }

        // Avoid unnecessary jumps on ground if you keep button pressed
        if (!_isJumping && _currentJumpsCount > 0)
        {
            _jumpAvailable = true; // if the button is never released
        }

        if (_isGrounded && !_isJumping)
        {
            _jumpAvailable = true;
            _currentJumpsCount = 0;
        }

        // Plunge after jump button releasing till zero y velocity
        if (!_isJumping && _rigidbody.velocity.y > 0 && !_isGrounded && _ascendTime > _minimalAscendTime)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y / 2);
        }

        if (_rigidbody.velocity.y < -12.0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -12.0f);
        }

        SetAnimationParameters();
        FlipSprite();

        _priorGrounded = _isGrounded;
    }

    protected virtual void OnJump()
    {

    }
}
