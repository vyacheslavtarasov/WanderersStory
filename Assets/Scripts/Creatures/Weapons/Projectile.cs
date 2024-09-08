using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public float Velocity = 1.0f;
    public float AngleDisplacement = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 direction = transform.localRotation * Vector3.down;
        Quaternion rotation = Quaternion.Euler(0, 0, AngleDisplacement);
        transform.rotation = rotation * transform.rotation;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = new Vector2(Velocity * direction.x, Velocity * direction.y);
    }
}
