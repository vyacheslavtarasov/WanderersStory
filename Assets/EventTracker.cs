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

    private GameObject _source;

    [Space]
    public UnityEvent EventInvoked;
    public InteractionEvent EventWithSource;

    public void OnEventInvokeInTargetObject()
    {
        EventInvoked?.Invoke();
        EventWithSource?.Invoke(_source);
    }

    public void Subscribe(GameObject obj)
    {
        _source = obj;
        var component = obj.GetComponent(ComponentName);

        FieldInfo field = component.GetType().GetField(EventName, BindingFlags.Public | BindingFlags.Instance);

        if (field != null)
        {

            UnityEvent unityEvent = field.GetValue(component) as UnityEvent;

            unityEvent.AddListener(OnEventInvokeInTargetObject);

        }

    }

    public void UnSubscribe(GameObject obj)
    {
        var component = obj.GetComponent(ComponentName);

        FieldInfo field = component.GetType().GetField(EventName, BindingFlags.Public | BindingFlags.Instance);

        if (field != null)
        {

            UnityEvent unityEvent = field.GetValue(component) as UnityEvent;

            unityEvent.RemoveListener(OnEventInvokeInTargetObject);

        }

    }

}
