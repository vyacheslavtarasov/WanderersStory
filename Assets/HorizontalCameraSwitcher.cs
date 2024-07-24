using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraSwitcher : MonoBehaviour
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
        _cameraStateController.SwitchToHorizontalCamera(gameObject);
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }
}
