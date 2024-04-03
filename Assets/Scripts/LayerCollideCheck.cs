using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCollideCheck : MonoBehaviour
{
    public float CastRadius = 1.0f;
    public Vector2 RelativePosition = Vector2.zero;
    public LayerMask Layers4Check;
    public float ForwardOffset = 0.0f;

    private Rigidbody2D _rigidbody;

    private bool _isCollided;

    private Vector2 _displacement = Vector2.zero;

    private Vector3 drawPositon = Vector3.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 castPosition = transform.position + new Vector3(RelativePosition.x, RelativePosition.y, 0.0f);
        Vector2 velocity = _rigidbody.velocity;
        // Debug.Log(velocity.normalized);
        if (velocity.magnitude > 0.0f && ForwardOffset != 0.0f) 
        {
            _displacement = velocity.normalized * ForwardOffset;
            castPosition = castPosition + new Vector3(_displacement.x, _displacement.y, 0.0f);
        }

        drawPositon = castPosition;
        var h = Physics2D.CircleCast(castPosition, CastRadius, Vector2.zero, 0, Layers4Check);
        if (h.collider != null)
        {
            _isCollided = true;
        }
        else
        {
            _isCollided = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _isCollided ? Color.green : Color.red;
        Gizmos.DrawSphere(drawPositon, CastRadius);
    }

    public bool GetCollisionStatus()
    {
        return _isCollided;
    }
}
