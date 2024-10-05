using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class CinemachineCameraShaker : MonoBehaviour
{
    // private CinemachineVirtualCamera _virtulaCamera;
    private CinemachineStateDrivenCamera _camerasController;
    private Hero _hero;
    public float ShakeTime = 10.0f;
    public float ShakeValue = 1.0f;

    private float _shakeTimer = 0.0f;
    private CinemachineBasicMultiChannelPerlin noise;
    void Awake()
    {
        // _virtulaCamera = FindObjectOfType<CinemachineVirtualCamera>();
        _camerasController = GetComponent<CinemachineStateDrivenCamera>();
        
        
    }


    [ContextMenu("shake")]
    public void StartShake()
    {
        _shakeTimer = ShakeTime;
        noise = _camerasController.LiveChild.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = ShakeValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_shakeTimer > 0.0f)
        {
            _shakeTimer -= 0.02f;
            if (_shakeTimer <= 0)
            {
                noise.m_AmplitudeGain = 0.0f;
            }
        } 
    }

    private void OnDestroy()
    {
        // _hero.GetComponent<Health>().OnDamage.RemoveListener(StartShake);
    }
}
