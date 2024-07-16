using Cinemachine;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _showCamera;
    [SerializeField] private CinemachineVirtualCamera _verticalCamera;
    [SerializeField] private CinemachineVirtualCamera _horizontalCamera;
    [SerializeField] private PolygonCollider2D _defaultCameraVolume;

    public GameObject CurrentCameraVolume = null;

    public void SetShowCameraPosition(Vector3 position)
    {
        position.z = _showCamera.transform.position.z;
        _showCamera.transform.position = position;
    }

    public void SetShowCamera()
    {
        _animator.SetBool("Show", true);
    }

    public void SetDefaultCamera()
    {
        _animator.SetBool("Show", false);
    }

    public void SwitchToVerticalCamera(PolygonCollider2D _collider, GameObject _object)
    {
        _verticalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _collider;
        _animator.SetTrigger("Vertical");
        CurrentCameraVolume = _object;

    }

    public void SwitchToHorizontalCamera(PolygonCollider2D _collider, GameObject _object)
    {
        _horizontalCamera.GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = _collider;
        _animator.SetTrigger("Horizontal");
        CurrentCameraVolume = _object;

    }

    public void TrySwitchDefault(GameObject _object)
    {
        if (CurrentCameraVolume != null && _object == CurrentCameraVolume)
        {
            _animator.SetTrigger("Default");
        }
    }
}
