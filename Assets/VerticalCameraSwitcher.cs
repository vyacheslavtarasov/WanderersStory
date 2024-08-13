using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] protected CameraStateController _cameraStateController;
    private void OnValidate()
    {
        if (_cameraStateController == null)
        {
            _cameraStateController = FindObjectOfType<CameraStateController>();
        }
    }

    public void AssignFollowObject(GameObject gameObject)
    {
        _cameraStateController.AssignFollowObject(gameObject);
    }

    public void AssignHeroFollow()
    {
        GameObject hero = FindObjectOfType<Hero>().gameObject;
        AssignFollowObject(hero);
    }
}


[RequireComponent(typeof(PolygonCollider2D))]
public class VerticalCameraSwitcher : CameraSwitcher
{
    /*[SerializeField] private CameraStateController _cameraStateController;

    private void OnValidate()
    {
        if (_cameraStateController == null)
        {
            _cameraStateController = FindObjectOfType<CameraStateController>();
        }
    }*/

    public void Switch()
    {
        Debug.Log("switching vertical");
        _cameraStateController.SwitchToVerticalCamera(gameObject);
    }

    public void SwitchOff()
    {
        _cameraStateController.TrySwitchDefault(gameObject);
    }
}
