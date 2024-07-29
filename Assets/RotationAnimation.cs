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

    public void RotateBack()
    {
        transform.DORotateQuaternion(InitialRotation, RotationTime);
    }

    public void Reset()
    {
        Reset();
    }

}
