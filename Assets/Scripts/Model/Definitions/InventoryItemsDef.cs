using System;
using UnityEngine;


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
    public string Name => _id;

    public bool IsVoid => string.IsNullOrEmpty(_id);
}

