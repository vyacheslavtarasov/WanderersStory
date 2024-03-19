using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    [SerializeField] private float _velocity;
    private Vector3 _direction;

    public void SetDirection(Vector3 newDirection)
    {
        _direction = newDirection;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += _velocity * _direction * Time.deltaTime;
    }
}
