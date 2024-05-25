using Cinemachine;
using UnityEngine;

public class CameraStateController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CinemachineVirtualCamera _showCamera;

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
}
