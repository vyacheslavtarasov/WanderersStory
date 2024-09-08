using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObjectRotation : StatefulObject
{
    private void Start()
    {
        SetCurrentState(DefaultState);
    }
    public override void ApplyCurrentState()
    {
        base.ApplyCurrentState();
        transform.rotation = Quaternion.Euler(0, 0, float.Parse(GetCurrentState()));
    }
}
