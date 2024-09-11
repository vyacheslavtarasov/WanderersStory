using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HorizontalMovementDetector : MonoBehaviour
{

    private Rigidbody2D _rigidbody;
    public bool _horizontalMove = false;
    public UnityEvent StartHorizontalMove;
    public UnityEvent StopHorizontalMove;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_rigidbody.velocity.x) > 0.01f && !_horizontalMove && Mathf.Abs(_rigidbody.velocity.y) < 0.01f) 
        {
            _horizontalMove = true;
            StartHorizontalMove?.Invoke();
        }

        if (Mathf.Abs(_rigidbody.velocity.x) < 0.01f && _horizontalMove)
        {
            _horizontalMove = false;
            StopHorizontalMove?.Invoke();
        }
    }
}
