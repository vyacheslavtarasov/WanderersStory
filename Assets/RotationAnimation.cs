using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationAnimation : MonoBehaviour
{
    public Vector3 TargetRotation;
    public float RotationTime = 10.0f;
    public void Rotate()
    {
        transform.DORotate(TargetRotation, RotationTime);
    }

    public void Reset()
    {
        Reset();
    }

}
