using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    protected string _currentState;
    public string DefaultState;

    public void SetCurrentState(float value)
    {
        _currentState = value.ToString();
    }

    public void SetCurrentState(string value)
    {
        if (gameObject.name == "Lever_Ready2bBroken")
        {
            Debug.Log("going to set current state " + value + " to " + gameObject.name);
        }
        
        _currentState = value;
    }

    public string GetCurrentState()
    {
        return _currentState;
    }

    public virtual void ApplyCurrentState()
    {
        if (GetCurrentState() == "dead")
        {
            Destroy(gameObject);
        }
    }
}
