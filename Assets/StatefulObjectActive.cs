using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObjectActive : StatefulObject
{
    private void Start()
    {
        SetCurrentState(DefaultState);
    }
    public override void ApplyCurrentState()
    {
        base.ApplyCurrentState();
        if (GetCurrentState() == "active")
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
