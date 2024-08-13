using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationAnimation : MonoBehaviour
{
    public Vector3 TargetRotation;
    public Quaternion InitialRotation;
    public float RotationTime = 10.0f;

    private void Start()
    {
        InitialRotation = transform.rotation;
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
