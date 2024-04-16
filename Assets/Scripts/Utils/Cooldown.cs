using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Cooldown
{
    [SerializeField] private float _value = 2.0f;

    private float _startTime;
    public void Reset() 
    {
        _startTime = Time.time + _value;
    }

    public bool IsReady() 
    {
        if (Time.time >= _startTime)
        {
            return true;
        }
        return false;
    }
}
