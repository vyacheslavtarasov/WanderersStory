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

#if UNITY_EDITOR
    public PerkDef[] ItemsForEditor => _perks;
#endif
}

[Serializable]
public struct PerkDef
{
    [SerializeField] private string _name;  // uniqie
    [SerializeField] private string _name2Display;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    public string Name => _name;
    public Sprite Icon => _icon;
    public string Description => _description;
    public string Name2Display => _name2Display;

    public bool IsVoid => string.IsNullOrEmpty(_name);
}

