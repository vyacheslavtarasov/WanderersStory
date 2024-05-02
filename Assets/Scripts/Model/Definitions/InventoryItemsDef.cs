using System;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


[CreateAssetMenu(menuName = "Defs/InventoryItems", fileName = "InventoryItems")]
public class InventoryItemsDef : ScriptableObject
{
    [SerializeField] private ItemDef[] _items;

    public ItemDef Get(string name)
    {
        foreach (var itemDef in _items)
        {
            if (itemDef.Name == name)
                return itemDef;
        }

        return default;
    }

#if UNITY_EDITOR
    public ItemDef[] ItemsForEditor => _items;
#endif
}

[Serializable]
public struct ItemDef
{
    [SerializeField] private string _id;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ItemTag[] _tags;

    public bool HasTag(ItemTag tag)
    {
        return _tags.Contains(tag);
    }
    public string Name => _id;
    public Sprite Icon => _icon;

    public bool IsVoid => string.IsNullOrEmpty(_id);
}

