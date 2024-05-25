using UnityEngine;

public class ShowTargetComponent : MonoBehaviour
{
    [SerializeField] private CameraStateController _cameraStateController;
    [SerializeField] private Transform _target;

    private void OnValidate()
    {
        if (_cameraStateController == null)
        {
            _cameraStateController = FindObjectOfType<CameraStateController>();
        }
    }

    public void ShowTarget(float showTime)
    {
        _cameraStateController.SetShowCameraPosition(_target.position);
        _cameraStateController.SetShowCamera();
        Invoke("MoveBack", showTime);
    }

    private void MoveBack()
    {
        _cameraStateController.SetDefaultCamera();
    }
}
