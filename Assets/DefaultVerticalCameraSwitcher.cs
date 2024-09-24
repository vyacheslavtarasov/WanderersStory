using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class DefaultVerticalCameraSwitcher : CameraSwitcher
{
    public void Switch()
    {
        _cameraStateController.SwitchToDefaultVerticalCamera(gameObject);
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }
}
