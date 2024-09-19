using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectActiveCondition : MonoBehaviour
{

    public UnityEvent ObjectActive;
    public UnityEvent ObjectNotActive;


    public void Check(GameObject ObjectToCheck)
    {
        if (ObjectToCheck.activeSelf)
        {
            ObjectActive?.Invoke();
            return;
        }
        ObjectNotActive?.Invoke();
    }
}