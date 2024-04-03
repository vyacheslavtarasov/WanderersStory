using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [Header("Movement adjustments")]
    [SerializeField] private float _runAccelerationMultiplier = 0.1f;
    [SerializeField] private float _maximumHorizontalVelocity = 4.5f;
    [SerializeField] private float _runBrakingMultiplier = 0.8f;
    
    [SerializeField] private float _runAccelerationMultiplierSlope = 0.5f;
    [SerializeField] private float _maximumHorizontalVelocitySlopeAscend = 4.5f;
    [SerializeField] private float _maximumHorizontalVelocitySlopeDescend = 4.5f;
    [SerializeField] private float _runBrakingMultiplierSlope = 0.1f;

    [Header("Jump adjustments")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _availableJumps = 2;
    [SerializeField] private float _minimalAscendTime = 1.0f;


    private int _currentJumpsCount;
    [SerializeField] private Sensor _interactSensor;


    private Vector3 _direction;

    private bool _fallDetected = false;

    private bool _isJumping = false; // if the jump pressed
    private bool _isGrounded = false; // if the hero is on the ground
    private bool _isGroundForward = false;
    private bool _jumpAvailable = true; // we need to avoid secondary jumps right after landing when jump button is kept pressed
    private float _fallTime = 0.0f;
    private float _ascendTime = 0.0f;
    private bool _priorGrounded = false;

    [SerializeField] private SpawnPrefab RunDustParticleSpawner;
    private bool _runParticleAvailable = false;
    

    // [SerializeField] private LayerCollideCheck GroundChecker;
    [SerializeField] private LayerCollideCheck ForwardGroundChecker;

    [SerializeField] private Sensor GroundChecker;


    private Rigidbody2D _rigidbody;
    private Animator _animator;
    [SerializeField]  private ParticleSystem _particleSystem;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;

        _currentJumpsCount = 0;
    }

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection;
    }

    public void SetJumping(bool isJumping)
    {
        _isJumping = isJumping;
    }

    public void Interact()
    {
        if (_interactSensor != null)
        {
            foreach(GameObject obj in _interactSensor.GetIntersectedObjects())
            {
                Interactive interactiveComponent = obj.GetComponent<Interactive>();
                interactiveComponent.Interact(this.gameObject);
            }
        }
    }

    public void OnChangeHealth(float wasHealth, float currentHealth, float overallHealth) 
    {
        if (wasHealth > currentHealth)
        {
            _animator.SetTrigger("hit");
            _particleSystem.Play();
        }
    }

    private void CheckIsGrounded()
    {
        _isGrounded = GroundChecker.GetCollisionStatus();
    }

    private void CheckGroundForward() 
    {
        _isGroundForward = ForwardGroundChecker.GetCollisionStatus();
    }

    private void SetAnimationParameters()
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

        if (_isJumping) // button is pressed
        {
            _animator.SetBool("isJumping", true);
        }
        else
        {
            _animator.SetBool("isJumping", false);
        }

        if (_isGrounded) // button is pressed
        {
            _animator.SetBool("isGrounded", true);
        }
        else
        {
            _animator.SetBool("isGrounded", false);
        }

        _animator.SetFloat("fallTime", _fallTime);
        _animator.SetFloat("ascendTime", _ascendTime);
    }

    private void LaunchParticles()
    {
        if (_runParticleAvailable && Mathf.Abs(_rigidbody.velocity.x) > 0.01f && _rigidbody.velocity.y == 0 && _direction.x != 0 && _isGrounded)
        {
            RunDustParticleSpawner.Spawn();
            _runParticleAvailable = false;
        }

        if (_rigidbody.velocity.x == 0 && _direction.x == 0 && _isGrounded)
        {
            _runParticleAvailable = true;
        }
        
    }

    private void FlipSprite()
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


    public void OnFall() {
    }
    private void FixedUpdate()
    {
        CheckIsGrounded(); // Now _airborne variable always show if the carrier is grounded.
        // CheckGroundForward();

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

        if (_isGrounded == false && _priorGrounded == true)
        {
            _ascendTime = 0.0f;
            _fallTime = 0.0f;
            if (!_isJumping) {
                if (_rigidbody.velocity.y > 0)
                {
                    _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0.0f);
                }else
                {
                    _rigidbody.velocity = new Vector2(0.0f, _rigidbody.velocity.y);
                }
                
            }
        }

        float accelerating = 0.0f;
        float breaking = 0.0f;
        float velocityLimit = 0.0f;

        accelerating = _runAccelerationMultiplier;
        breaking = _runBrakingMultiplier;

        bool onPlane = false;
        bool ascendSlope = false;

        if (Mathf.Abs(_rigidbody.velocity.x) > 0.02f && Mathf.Abs(_rigidbody.velocity.y) > 0.02f && _isGrounded)
        {
            accelerating = _runAccelerationMultiplierSlope;
            breaking = _runBrakingMultiplierSlope;
            onPlane = false;
            if (_rigidbody.velocity.y > 0) {
                ascendSlope = true;
                velocityLimit = _maximumHorizontalVelocitySlopeAscend;
            }
            else
            {
                ascendSlope = false;
                velocityLimit = _maximumHorizontalVelocitySlopeDescend;
            }
        }
        else
        {
            onPlane = true;
            accelerating = _runAccelerationMultiplier;
            breaking = _runBrakingMultiplier;
            velocityLimit = _maximumHorizontalVelocity;
        }



        // Horizontal movement.
        // _rigidbody.velocity = new Vector2(_direction.x * 3.0f, _rigidbody.velocity.y );

        float absoluteXVelocity = Mathf.Abs(_rigidbody.velocity.x);
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
            if (absoluteXVelocity > 0.1f)
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



        // Jumping
        // The firt jump must alway be from the ground.
        if (_isJumping  && _jumpAvailable && _isGrounded)
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            _currentJumpsCount = _availableJumps - 1;
            _jumpAvailable = false;
        }

        if (_isJumping && _currentJumpsCount > 0 && _jumpAvailable && !_isGrounded )
        {
            _rigidbody.velocity = new Vector2(this._rigidbody.velocity.x, 0);
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            _currentJumpsCount -= 1;
            _jumpAvailable = false;
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
        if ( !_isJumping && _rigidbody.velocity.y > 0 && !_isGrounded && _ascendTime > _minimalAscendTime)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y / 2);
        }

        SetAnimationParameters();
        FlipSprite();
        LaunchParticles();
        _priorGrounded = _isGrounded;
    }
}
