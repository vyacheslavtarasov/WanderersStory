using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public float _health;
    public int JumpsAmount;
    public List<InventoryItemData> Inventory;
    public int QuickInventoryIndex = 0;

    public delegate void OnHealthChanged(float newValue);
    public event OnHealthChanged OnChanged;

    public Action OnInventoryChanged;

    public float Health
    {
        get => _health;
        set
        {
            _health = value;

            OnChanged?.Invoke(value);
        }
    }

    public void UpdateInventory(List<InventoryItemData> inventory) 
    {
        Inventory = new List<InventoryItemData>(inventory);
        OnInventoryChanged?.Invoke();
    }

    public PlayerData ShallowCopy()
    {
        return (PlayerData)this.MemberwiseClone();
    }
}
