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

[Serializable]
public struct InventoryItemData
{
    public string Name;
    public int Amount;

    public InventoryItemData(string name, int amount)
    {
        this.Name = name;
        this.Amount = amount;
    }

    public bool IsVoid => string.IsNullOrEmpty(Name);
}
