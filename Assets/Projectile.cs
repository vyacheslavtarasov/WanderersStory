using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Velocity = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        float direction = transform.lossyScale.x > 0.0f ? 1.0f : -1.0f;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(Velocity * direction, _rigidbody.velocity.y);
    }
}
