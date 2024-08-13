using Cinemachine;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _showCamera;
    [SerializeField] private CinemachineVirtualCamera _verticalCamera;
    [SerializeField] private CinemachineVirtualCamera _horizontalCamera;
    [SerializeField] private CinemachineVirtualCamera _staticCamera;
    [SerializeField] private CinemachineVirtualCamera _defaultCamera;
    [SerializeField] private CinemachineVirtualCamera _defaultVerticalCamera;

    [SerializeField] private PolygonCollider2D _defaultCameraVolume;

    private CinemachineVirtualCamera CurrentCinemachineVirtualCamera;
    private CinemachineVirtualCamera PreviousCinemachineVirtualCamera;

    private void Start()
    {
        CurrentCinemachineVirtualCamera = _defaultCamera;
        PreviousCinemachineVirtualCamera = _defaultCamera;
    }

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
        Debug.Log("setting show camera");
        _animator.SetTrigger("Show");
    }

    public void SetPreviousCamera()
    {
        _animator.SetTrigger(currentCamera);
    }

    public void AssignFollowObject(GameObject gameObject)
    {
        Debug.Log("assigning object");
        Debug.Log(CurrentCinemachineVirtualCamera);
        Debug.Log(gameObject);
        CurrentCinemachineVirtualCamera.GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform;
    }

    public void AssignHeroFollowObject()
    {
        CurrentCinemachineVirtualCamera.GetComponent<AssignHeroToFollowCamera>().AssignHero();
    }

    public void SwitchToVerticalCamera(GameObject _object)
    {
        _verticalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        PreviousCinemachineVirtualCamera = CurrentCinemachineVirtualCamera;
        CurrentCinemachineVirtualCamera = _verticalCamera;
        _animator.SetTrigger("Vertical");
        previousCamera = currentCamera;
        currentCamera = "Vertical";
    }

    public void SwitchToHorizontalCamera(GameObject _object)
    {
        _horizontalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        PreviousCinemachineVirtualCamera = CurrentCinemachineVirtualCamera;
        CurrentCinemachineVirtualCamera = _horizontalCamera;
        _animator.SetTrigger("Horizontal");
        previousCamera = currentCamera;
        currentCamera = "Horizontal";

    }

    public void SwitchToStaticlCamera(GameObject _object)
    {
        _staticCamera.transform.position = _object.transform.position;
        _staticCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        PreviousCinemachineVirtualCamera = CurrentCinemachineVirtualCamera;
        CurrentCinemachineVirtualCamera = _staticCamera;
        _animator.SetTrigger("Static");
        previousCamera = currentCamera;
        currentCamera = "Static";
    }

    public void SwitchToDefaultVerticalCamera(GameObject _object)
    {
        _staticCamera.transform.position = _object.transform.position;
        _staticCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _object.GetComponent<PolygonCollider2D>();
        PreviousCinemachineVirtualCamera = CurrentCinemachineVirtualCamera;
        CurrentCinemachineVirtualCamera = _defaultVerticalCamera;
        _animator.SetTrigger("DefaultVertical");
        previousCamera = currentCamera;
        currentCamera = "DefaultVertical";
    }

    public void TrySwitchDefault(GameObject _object)
    {
        var liveCamera = GetComponent<CinemachineStateDrivenCamera>().LiveChild;
        var myObject = liveCamera.VirtualCameraGameObject.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D.gameObject;

        if (_object == myObject)
        {
            _animator.SetTrigger(previousCamera);
            currentCamera = previousCamera;
            PreviousCinemachineVirtualCamera = CurrentCinemachineVirtualCamera;
        }
        previousCamera = "Default";
        CurrentCinemachineVirtualCamera = _defaultCamera;
    }
}
