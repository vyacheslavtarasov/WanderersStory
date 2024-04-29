using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public float _health;
    public int JumpsAmount;
    public List<InventoryItemData> Inventory;

    public delegate void OnHealthChanged(float newValue);
    public event OnHealthChanged OnChanged;

    public float Health
    {
        get => _health;
        set
        {
            _health = value;

            OnChanged?.Invoke(value);
        }
    }

    public PlayerData ShallowCopy()
    {
        return (PlayerData)this.MemberwiseClone();
    }
}
