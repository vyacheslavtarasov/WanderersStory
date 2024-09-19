using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] ObjectsToEnable;

    void Awake()
    {
        ObjectsToEnable = GetComponentsInChildren<Transform>(true);
    }

    public void TurnOn()
    {
        foreach (Transform obj in ObjectsToEnable)
        {
            obj.gameObject.SetActive(true);
        }
    }

    public void TurnOff()
    {
        foreach (Transform obj in ObjectsToEnable)
        {
            // Debug.Log(obj.gameObject.name);
            obj.gameObject.SetActive(false);
        }
    }
}
