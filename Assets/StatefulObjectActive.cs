using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObjectActive : StatefulObject
{
    private void Awake()
    {
        if (gameObject.name == "Lever_Ready2bBroken")
        {
            // Debug.Log("Awake for Lever.. ");
        }
        if (_currentState == null || _currentState == "")
        {
            if (gameObject.name == "Lever_Ready2bBroken")
            {
                // Debug.Log("Awake for Lever.. going to set default state as current state");
            }
            SetCurrentState(DefaultState);
        }
    }
    public override void ApplyCurrentState()
    {
        base.ApplyCurrentState();
        if (gameObject.name == "Lever_Ready2bBroken")
        {
            // Debug.Log("going to apply " + GetCurrentState());
        }
        
        if (GetCurrentState() == "active")
        {
            gameObject.SetActive(true);
            // Debug.Log(gameObject.name + " was set to active");
        }
        else
        {
            gameObject.SetActive(false);
            // Debug.Log(gameObject.name + " was set to notActive");
        }
    }
}
