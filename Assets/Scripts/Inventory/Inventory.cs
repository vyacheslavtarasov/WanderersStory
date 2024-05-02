using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<InventoryItemData> _inventory = new List<InventoryItemData>();

    public delegate void OnInventoryChanged(List<InventoryItemData> _inventory);

    public OnInventoryChanged OnChanged;

    public void Add(string name, int amount)
    {
        if (amount <= 0) return;
        var itemDef = DefsFacade.I.Items.Get(name);
        if (itemDef.IsVoid) return;

        InventoryItemData item;
        int itemIndex = GetItemIndex(name);
        if (itemIndex < 0)
        {
            item = new InventoryItemData(name, amount);
            _inventory.Add(item);
        }
        else
        {
            item = _inventory[itemIndex];
            item.Amount += 1;
            Debug.Log(item.QuickMenuIndex);
            _inventory[itemIndex] = item;
        }

        OnChanged?.Invoke(_inventory);
    }

    public void SetDirty()
    {
        OnChanged?.Invoke(_inventory);
    }

    public void SetInventory(List<InventoryItemData> inventory)
    {
        _inventory = new List<InventoryItemData>(inventory);
    }

    public void Remove(string name, int amount)
    {
        var itemDef = DefsFacade.I.Items.Get(name);
        if (itemDef.IsVoid) return;

        var item = GetItem(name);
        if (item.IsVoid) return;

        item.Amount -= amount;

        if (item.Amount <= 0)
            _inventory.Remove(item);

        OnChanged?.Invoke(_inventory);
    }

    private InventoryItemData GetItem(string name)
    {
        foreach (var itemData in _inventory)
        {
            if (itemData.Name == name)
                return itemData;
        }

        return new InventoryItemData(null, 1);
    }

    private int GetItemIndex(string name)
    {
        for(var i = 0; i < _inventory.Count; i++)
        {
            if (_inventory[i].Name == name)
                return i;
        }
        return -1;
    }

    public int Count(string name)
    {
        var count = 0;
        foreach (var item in _inventory)
        {
            if (item.Name == name)
                count += item.Amount;
        }

        return count;
    }


    /*[Serializable]
    public class InventoryItemData : ICloneable
    {
        public string Name;
        public int Amount;

        public InventoryItemData(string name)
        {
            Name = name;
        }

        public InventoryItemData(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public object Clone()
        {
            return new InventoryItemData(Name, Amount);
        }
    }*/

    
}
