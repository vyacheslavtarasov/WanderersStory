using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class VerticalCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CameraStateController _cameraStateController;

    

    private void OnValidate()
    {
        if (_cameraStateController == null)
        {
            _cameraStateController = FindObjectOfType<CameraStateController>();
        }
    }

    public void Switch()
    {
        _cameraStateController.SwitchToVerticalCamera(gameObject);
        _cameraStateController.CurrentCameraVolume = gameObject;
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }



}
