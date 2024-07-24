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
        _verticalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        _animator.SetTrigger("Vertical");
        previousCamera = currentCamera;
        currentCamera = "Vertical";
    }

    public void SwitchToHorizontalCamera(GameObject _object)
    {
        _horizontalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        _animator.SetTrigger("Horizontal");
        previousCamera = currentCamera;
        currentCamera = "Horizontal";

    }

    public void SwitchToStaticlCamera(GameObject _object)
    {
        _staticCamera.transform.position = _object.transform.position;
        _staticCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        _animator.SetTrigger("Static");
        previousCamera = currentCamera;
        currentCamera = "Static";
    }

    public void TrySwitchDefault(GameObject _object)
    {
        var liveCamera = GetComponent<CinemachineStateDrivenCamera>().LiveChild;
        var myObject = liveCamera.VirtualCameraGameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D.gameObject;

        if (_object == myObject)
        {
            _animator.SetTrigger(previousCamera);
            currentCamera = previousCamera;
        }
        previousCamera = "Default";
    }
}
