using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationAnimation : MonoBehaviour
{
    public bool InfiniteRotation = false;
    private Rigidbody2D _rigidbody;

    [NaughtyAttributes.ShowIf("InfiniteRotation")]
    public float RotationVelocity;


    [NaughtyAttributes.HideIf("InfiniteRotation")]
    public Vector3 TargetRotation;
    [NaughtyAttributes.HideIf("InfiniteRotation")]
    public Quaternion InitialRotation;
    [NaughtyAttributes.HideIf("InfiniteRotation")]
    public float RotationTime = 10.0f;

    private void Start()
    {
        InitialRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void InvertRotationVelocity()
    {
        RotationVelocity = -RotationVelocity;
    }

    private void Update()
    {
        if (InfiniteRotation)
        {
            _rigidbody.angularVelocity = RotationVelocity;
        }
    }

    private void OnDisable()
    {
        _rigidbody.angularVelocity = 0.0f;
    }
    public void Rotate()
    {
        transform.DORotate(TargetRotation, RotationTime);
    }

    public void RotateWithDelay(float delay = 0f)
    {
        Debug.Log("rotating with delay");
        Invoke("Rotate", delay);
    }

    public void RotateBack()
    {
        transform.DORotateQuaternion(InitialRotation, RotationTime);
    }

    public void RotateBackWithDelay(float delay = 0f)
    {
        Invoke("RotateBack", delay);
    }

    public void Reset()
    {
        Reset();
    }

}
