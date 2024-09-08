using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCondition : MonoBehaviour
{
    public Vector3 Value;

    [Space]
    public InteractionEvent Equal;
    public InteractionEvent NotEqual;


    public void CheckDirection(GameObject obj)
    {
        if (obj.transform.localScale == Value)
        {
            Equal?.Invoke(obj);
        }
        else
        {
            NotEqual?.Invoke(obj);
        }
    }
}