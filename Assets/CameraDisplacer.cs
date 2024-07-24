using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraDisplacer : MonoBehaviour
{
    public Vector3 Displacement = Vector3.zero;
    public float DisplaceTime = 1.0f;

    public void SetDisplacement(Vector3 displacement)
    {
        CinemachineCameraOffset[] offsets = FindObjectsOfType<CinemachineCameraOffset>();

        foreach (CinemachineCameraOffset offset in offsets)
        {
            DOTween.To(() => offset.m_Offset, x => offset.m_Offset = x, displacement, 1);
        }
    }

    public void SetDisplacement()
    {
        SetDisplacement(Displacement);
    }
    public void RollbackDisplacementToZero()
    {
        SetDisplacement(Vector3.zero);
    }
}
