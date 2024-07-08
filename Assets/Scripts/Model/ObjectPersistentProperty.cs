using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

[Serializable]
public class ObjectPersistentProperty : PersistentProperty<PlayerData>
{

    public ObjectPersistentProperty(string key, PlayerData defaultValue) : base(key, defaultValue)
    {
        Init();
    }

    protected override PlayerData Read(string Key, PlayerData defaultValue)
    {

        return JsonConvert.DeserializeObject<PlayerData>(PlayerPrefs.GetString(Key, ""));
    }

    protected override void Write(string Key, PlayerData value)
    {

        string jsonString = JsonConvert.SerializeObject(value);

        PlayerPrefs.SetString(Key, jsonString);
        PlayerPrefs.Save();
    }
}

