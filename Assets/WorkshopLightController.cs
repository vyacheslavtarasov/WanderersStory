using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkshopLightController : MonoBehaviour
{
    private TorchController[] _torchControllers;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(gameObject.name);
        _torchControllers = transform.GetComponentsInChildren<TorchController>(true);
        TurnOff();
    }

    public void TurnOn()
    {
        // Debug.Log("here");
        foreach (TorchController obj in _torchControllers)
        {
            obj.TurnOn();
        }
    }

    public void TurnOff()
    {
        // Debug.Log("turning off");
        foreach (TorchController obj in _torchControllers)
        {
            obj.TurnOff();
        }
    }

}
