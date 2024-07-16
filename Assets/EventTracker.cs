using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;

public class EventTracker : MonoBehaviour
{
    [Header("Name of the Component")]
    public string ComponentName;

    [Header("Name of the Event")]
    public string EventName;

    [Space]
    public UnityEvent EventInvoked;

    public void OnEventInvokeInTargetObject()
    {
        EventInvoked?.Invoke();
    }

    public void Subscribe(GameObject obj)
    {
        var component = obj.GetComponent(ComponentName);
        Debug.Log(component.GetType());

        FieldInfo field = component.GetType().GetField(EventName, BindingFlags.Public | BindingFlags.Instance);
        Debug.Log(field);

        if (field != null)
        {

            UnityEvent unityEvent = field.GetValue(component) as UnityEvent;

            unityEvent.AddListener(OnEventInvokeInTargetObject);

        }

    }

    public void UnSubscribe(GameObject obj)
    {
        var component = obj.GetComponent(ComponentName);
        Debug.Log(component.GetType());

        FieldInfo field = component.GetType().GetField(EventName, BindingFlags.Public | BindingFlags.Instance);
        Debug.Log(field);

        if (field != null)
        {

            UnityEvent unityEvent = field.GetValue(component) as UnityEvent;

            unityEvent.RemoveListener(OnEventInvokeInTargetObject);

        }

    }

}
