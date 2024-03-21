using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerCollideCheck : MonoBehaviour
{
    public float CastRadius = 1.0f;
    public Vector2 RelativePosition = Vector2.zero;
    public LayerMask Layers4Check;

    private bool _isCollided;

    private void FixedUpdate()
    {
        var h = Physics2D.CircleCast(transform.position + new Vector3(RelativePosition.x, RelativePosition.y, 0.0f), CastRadius, Vector2.zero, 0, Layers4Check);
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
        Gizmos.DrawSphere(transform.position + new Vector3(RelativePosition.x, RelativePosition.y, 0.0f), CastRadius);
    }

    public bool GetCollisionStatus()
    {
        return _isCollided;
    }
}
