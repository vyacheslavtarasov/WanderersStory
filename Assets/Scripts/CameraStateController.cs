using Cinemachine;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _showCamera;
    [SerializeField] private CinemachineVirtualCamera _verticalCamera;
    [SerializeField] private CinemachineVirtualCamera _horizontalCamera;
    [SerializeField] private CinemachineVirtualCamera _staticCamera;

    [SerializeField] private PolygonCollider2D _defaultCameraVolume;

    public GameObject CurrentCameraVolume = null;

    public string previousCamera = "Default";
    public string currentCamera = "Default";

    public void SetShowCameraPosition(Vector3 position)
    {
        position.z = _showCamera.transform.position.z;
        _showCamera.transform.position = position;
    }

    public void SetShowCamera()
    {
        _animator.SetTrigger("Show");
    }

    public void SetPreviousCamera()
    {
        _animator.SetTrigger(currentCamera);
    }

    public void SwitchToVerticalCamera(GameObject _object)
    {
        _verticalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();;
        _animator.SetTrigger("Vertical");
        previousCamera = currentCamera;
        currentCamera = "Vertical";
        CurrentCameraVolume = _object;
    }

    public void SwitchToHorizontalCamera(PolygonCollider2D _collider, GameObject _object)
    {
        _horizontalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _collider;
        _animator.SetTrigger("Horizontal");
        previousCamera = currentCamera;
        currentCamera = "Horizontal";
        CurrentCameraVolume = _object;

    }

    public void SwitchToStaticlCamera(GameObject _object)
    {
        _staticCamera.transform.position = _object.transform.position;
        _animator.SetTrigger("Static");
        previousCamera = currentCamera;
        currentCamera = "Static";
        CurrentCameraVolume = _object;
    }

    public void TrySwitchDefault(GameObject _object)
    {
        if (previousCamera == "Default") // because volume can be uncorrect after here-there transfer.
        {
            _animator.SetTrigger("Default");
            currentCamera = "Default";
            return;
        }

        if (CurrentCameraVolume != null && _object == CurrentCameraVolume)
        {
            _animator.SetTrigger(previousCamera);
            currentCamera = previousCamera;
        }
        previousCamera = "Default";
    }
}
