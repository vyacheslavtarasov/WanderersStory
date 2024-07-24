using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalCameraSwitcher : MonoBehaviour
{
    [SerializeField] private CameraStateController _cameraStateController;
    [SerializeField] private PolygonCollider2D _collider;

    private void OnValidate()
    {
        if (_cameraStateController == null)
        {
            _cameraStateController = FindObjectOfType<CameraStateController>();
        }
        _collider = GetComponent<PolygonCollider2D>();
    }

    public void Switch()
    {
        _cameraStateController.SwitchToHorizontalCamera(_collider, gameObject);
        _cameraStateController.CurrentCameraVolume = gameObject;
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }



}
