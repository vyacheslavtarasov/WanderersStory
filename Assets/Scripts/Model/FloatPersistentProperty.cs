using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FloatPersistenProperty : PersistentProperty<float>
{
    
    public FloatPersistenProperty(string key, float defaultValue) : base(key, defaultValue)
    {
        Init();
    }

    protected override float Read(string Key, float defaultValue)
    {
        return PlayerPrefs.GetFloat(Key, defaultValue);
    }

    protected override void Write(string Key, float value)
    {
        PlayerPrefs.SetFloat(Key, value);
        PlayerPrefs.Save();
    }
}
