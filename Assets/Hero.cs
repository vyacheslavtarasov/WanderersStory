using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _velocity;
    private Vector3 _direction;

    [SerializeField] private float _jumpForce;

    private bool _isJumping = false; // if the jump pressed
    private bool _isGrounded = false; // if the hero is on the ground
    private bool _jumpAvailable = true; // we need to avoid secondary jumps right after landing when jump button is kept pressed

    [SerializeField] private LayerCollideCheck GroundChecker;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        CheckIsGrounded(); // Now _airborne variable always show if the carrier is grounded.

        // Horizontal movement
        _rigidbody.velocity = new Vector2(_direction.x * _velocity, _rigidbody.velocity.y );

        // Jumping
        if (_isJumping && _isGrounded && _jumpAvailable)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Force);
            _jumpAvailable = false;
        }

        // Plunge after jump button releasing
        if ( !_isJumping && _rigidbody.velocity.y > 0)
        {
            _rigidbody.velocity /= 2;
        }

        // Avoid unnecessary jumps
        if (!_isJumping && _isGrounded)
        {
            _jumpAvailable = true;
        }

    }
}
