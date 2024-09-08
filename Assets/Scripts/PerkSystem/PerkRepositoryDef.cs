using System;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[CreateAssetMenu(menuName = "Defs/PerkRepository", fileName = "PerkRepository")]
public class PerkRepositoryDef : ScriptableObject
{
    [SerializeField] private PerkDef[] _perks;

    public PerkDef Get(string name)
    {
        foreach (var perkDef in _perks)
        {
            if (perkDef.Name == name)
                return perkDef;
        }

        return default;
    }

    /*public DialogEntry GetLocalization(string name)
    {
        for (int i = 0; i < _perks.Length; i++)
        {
            if (_perks[i].Name == name)
            {
                Debug.Log("returning" + _localization[i]);
                return _localization[i];
            }

        }
        return _localization[0];
    }*/

    // [SerializeField] public List<DialogEntry> _localization;

#if UNITY_EDITOR
    public PerkDef[] ItemsForEditor => _perks;
#endif
}

[Serializable]
public struct PerkDef
{
    [SerializeField] private string _name;  // uniqie
    [SerializeField] private Sprite _icon;
    [SerializeField] public DialogEntry _nameAndDescription;

    public string Name => _name;
    public Sprite Icon => _icon;



    public bool IsVoid => string.IsNullOrEmpty(_name);
}

