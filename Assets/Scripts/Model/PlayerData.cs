using System.Collections.Generic;
using System;
using System;
using Newtonsoft.Json;

[Serializable]
public class PlayerData
{
    public float _health;
    public int JumpsAmount;
    public List<InventoryItemData> Inventory;
    public int QuickInventoryIndex = 0;
    public List<PlayerPerk> Perks;
    public string LevelName; // I am going to save this object between game sessions and I need this to load a correct level

    
    public delegate void OnHealthChanged(float newValue);

    public event OnHealthChanged OnChanged;

    [JsonIgnore]
    public Action OnInventoryChanged;

    [JsonIgnore]
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
