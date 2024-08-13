using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Mode
{
    Circle,
    PingPong,
    OneWay,
    OneStepAtATime
}

public class MovingPlatformController : MonoBehaviour
{

    public float Velocity = 1.0f;
    public List<Transform> Positions = new List<Transform>();

    public int _targetPositionIndex = 1;
    public bool _directionForward = true;

    public delegate void OnHealthChanged(Vector2 newValue);
    public event OnHealthChanged OnMove;

    public Mode MoveMode = Mode.PingPong;

    public Rigidbody2D _rigidbody;

    public void SetVelocity(float velocity)
    {
        Velocity = velocity;
    }

    public void SetTargetPosition(int position)
    {
        _targetPositionIndex = position;
    }

    // Start and stop movement by enabling and disabling the controller
    private void Awake()
    {

        _rigidbody = GetComponent<Rigidbody2D>();

    }




    private void FixedUpdate()
    {


        if (Vector2.Distance(transform.position, Positions[_targetPositionIndex].position) < Velocity * 0.02f)
        {
            if (MoveMode == Mode.PingPong)
            {
                if (_directionForward)
                {
                    _targetPositionIndex += 1;
                    if (_targetPositionIndex >= Positions.Count)
                    {
                        _directionForward = false;
                        _targetPositionIndex = Positions.Count - 2;
                    }
                }
                else
                {
                    _targetPositionIndex -= 1;
                    if (_targetPositionIndex < 0)
                    {
                        _directionForward = true;
                        _targetPositionIndex = 1;
                    }
                }
            }
            else if (MoveMode == Mode.Circle)
            {
                if (_directionForward)
                {
                    _targetPositionIndex += 1;
                    if (_targetPositionIndex >= Positions.Count)
                    {
                        _targetPositionIndex = 0;
                    }
                }
                else
                {
                    _targetPositionIndex -= 1;
                    if (_targetPositionIndex < 0)
                    {
                        _targetPositionIndex = Positions.Count - 1;
                    }
                }
            }
            else if (MoveMode == Mode.OneWay)
            {
                if (_directionForward)
                {
                    _targetPositionIndex += 1;
                    if (_targetPositionIndex >= Positions.Count)
                    {
                        _targetPositionIndex = Positions.Count - 2;
                        _directionForward = false;
                        Velocity = 0.0f;
                        this.enabled = false;
                    }
                }
                else
                {
                    _targetPositionIndex -= 1;
                    if (_targetPositionIndex < 0)
                    {
                        _targetPositionIndex = 1;
                        _directionForward = true;
                        Velocity = 0.0f;
                        this.enabled = false;
                    }
                }
            }
            if (MoveMode == Mode.OneStepAtATime)
            {
                if (_directionForward)
                {
                    _targetPositionIndex += 1;
                    if (_targetPositionIndex >= Positions.Count)
                    {
                        _targetPositionIndex = Positions.Count - 2;
                        _directionForward = false;
                    }
                }
                else
                {
                    _targetPositionIndex -= 1;
                    if (_targetPositionIndex < 0)
                    {
                        _targetPositionIndex = 1;
                        _directionForward = true;
                        
                    }
                }
                Velocity = 0.0f;
                this.enabled = false;
            }
        }

        _rigidbody.velocity = (Positions[_targetPositionIndex].position - transform.position).normalized * Velocity;

    }

    private void LateUpdate()
    {
        
    }

}
