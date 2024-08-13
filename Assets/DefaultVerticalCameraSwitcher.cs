using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DefaultVerticalCameraSwitcher : CameraSwitcher
{
    public void Switch()
    {
        Debug.Log("switching default vertical");
        _cameraStateController.SwitchToDefaultVerticalCamera(gameObject);
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }
}
