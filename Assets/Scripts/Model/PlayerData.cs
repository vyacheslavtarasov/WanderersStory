using System.Collections.Generic;
using System;
using Newtonsoft.Json;

[Serializable]
public class ObjectState
{
    public string ObjectName;
    public string DefaultState;
    public string CurrentState;

    public ObjectState(string objectName, string defaultState, string currentState)
    {
        ObjectName = objectName;
        DefaultState = defaultState;
        CurrentState = currentState;
    }
}

[Serializable]
public class PlayerData
{
    public float _health;
    public int JumpsAmount;
    public List<InventoryItemData> Inventory;
    public int QuickInventoryIndex = 0;
    public List<PlayerPerk> Perks;
    public int _money = 0;
    public string CheckpointName = "default";
    public List<ObjectState> LevelObjectsState;
    
    public string LevelName; // I am going to save this object between game sessions and I need this to load a correct level

    
    public delegate void OnHealthChanged(float newValue);

    public event OnHealthChanged OnChanged;

    public delegate void OnMoneyChanged(int newValue);

    public event OnMoneyChanged OnMoneyChangedEvent;
    public int Money {get => _money;
        set {
            _money = value;
            OnMoneyChangedEvent?.Invoke(value);
        } }

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

    public InventoryItemData GetInventoryItem(string Name)
    {
        foreach(InventoryItemData item in Inventory)
        {
            if (item.Name == Name)
            {
                return item;
            }
        }
        return new InventoryItemData(null, 0, 0);
    }

    public PlayerData ShallowCopy()
    {
        return (PlayerData)this.MemberwiseClone();
    }
}
