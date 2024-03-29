using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _runAccelerationMultiplier = 0.1f;
    [SerializeField] private float _maximumHorizontalVelocity = 4.5f;
    [SerializeField] private float _runBrakingMultiplier = 0.9f;
    [SerializeField] private float _minimalAscendTime = 1.0f;

    [SerializeField] private int _availableJumps = 2;
    public int _currentJumpsCount;

    private Vector3 _direction;

    [SerializeField] private float _jumpForce;

    private bool _isJumping = false; // if the jump pressed
    private bool _isGrounded = false; // if the hero is on the ground
    private bool _jumpAvailable = true; // we need to avoid secondary jumps right after landing when jump button is kept pressed
    private float _fallTime = 0.0f;
    private float _ascendTime = 0.0f;
    private bool _priorGrounded = false;
    

    [SerializeField] private LayerCollideCheck GroundChecker;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
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

    private void CheckIsGrounded()
    {
        _isGrounded = GroundChecker.GetCollisionStatus();
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

    private void FlipSprite()
    {
        if (_direction.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_direction.x > 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private void FixedUpdate()
    {
        CheckIsGrounded(); // Now _airborne variable always show if the carrier is grounded.

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
        }


        // Horizontal movement.
        // _rigidbody.velocity = new Vector2(_direction.x * 3.0f, _rigidbody.velocity.y );

        float absoluteXVelocity = Mathf.Abs(_rigidbody.velocity.x);
        Vector2 newVelocity = Vector2.zero;

        if (_direction.x >= 0.01f)
        {
            newVelocity = new Vector2(_direction.x * (_maximumHorizontalVelocity - _rigidbody.velocity.x) * _runAccelerationMultiplier + _rigidbody.velocity.x, _rigidbody.velocity.y);

            if (Mathf.Abs(_maximumHorizontalVelocity - _rigidbody.velocity.x) < 0.1f)
            {
                newVelocity = new Vector2(_maximumHorizontalVelocity, _rigidbody.velocity.y);
            }

        }
        
        if (_direction.x <= -0.01f)
        {
            newVelocity = new Vector2(-_direction.x * (-_maximumHorizontalVelocity - _rigidbody.velocity.x) * _runAccelerationMultiplier + _rigidbody.velocity.x, _rigidbody.velocity.y);

            if (Mathf.Abs(_maximumHorizontalVelocity - _rigidbody.velocity.x) < 0.1f)
            {
                newVelocity = new Vector2(-_maximumHorizontalVelocity, _rigidbody.velocity.y);
            }
        }

        if (_direction.x < 0.01f && _direction.x > -0.01f)
        {
            if (absoluteXVelocity > 1.0f)
            {
                newVelocity = new Vector2(_rigidbody.velocity.x * _runBrakingMultiplier, _rigidbody.velocity.y);
            }
            else
            {
                newVelocity = new Vector2(0.0f, _rigidbody.velocity.y);
            }
        }

        _rigidbody.velocity = newVelocity;

        

        // Jumping
        // The firt jump must alway be from the ground.
        if (_isJumping  && _jumpAvailable && _isGrounded)
        {
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

        if (_rigidbody.velocity.y < 0.1f && !_isGrounded)
        {
            _rigidbody.gravityScale = 3.5f;
        }
        else
        {
            _rigidbody.gravityScale = 2.0f;
        }
        // Avoiding slipping from slipper slopes
        if (_isGrounded && _direction.x == 0.0f && !_isJumping)
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
        }

       

        

        SetAnimationParameters();
        FlipSprite();
        _priorGrounded = _isGrounded;
    }
}
