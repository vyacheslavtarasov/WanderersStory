using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCameraSwitcher : MonoBehaviour
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
        _cameraStateController.SwitchToStaticlCamera(gameObject);
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }
}