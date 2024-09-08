using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    private string _currentState;
    public string DefaultState;

    public void SetCurrentState(float value)
    {
        _currentState = value.ToString();
    }

    public void SetCurrentState(string value)
    {
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
