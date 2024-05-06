using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StringPersistentProperty : PersistentProperty<string>
{

    public StringPersistentProperty(string key, string defaultValue) : base(key, defaultValue)
    {
        Init();
    }

    protected override string Read(string Key, string defaultValue)
    {
        return PlayerPrefs.GetString(Key, defaultValue);
    }

    protected override void Write(string Key, string value)
    {
        PlayerPrefs.SetString(Key, value);
        PlayerPrefs.Save();
    }
}
