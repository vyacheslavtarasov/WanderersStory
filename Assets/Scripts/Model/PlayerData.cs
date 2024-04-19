using System.Collections.Generic;
using System;

[Serializable]
public class PlayerData
{
    public float Health;
    public int JumpsAmount;
    public List<InventoryItemData> Inventory;

    public PlayerData ShallowCopy()
    {
        return (PlayerData)this.MemberwiseClone();
    }
}
