using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct PlayerPerk
{
    public string Name;
    public bool Active;
    public int MenuIndex;

    public PlayerPerk(string name, bool active = false, int menuIndex = -1)
    {
        this.Name = name;
        this.Active = active;
        this.MenuIndex = menuIndex;
    }

    public bool IsVoid => string.IsNullOrEmpty(Name);
}

[Serializable]
public struct PerkShopPerk
{
    public string Name;
    public int Price;

    public PerkShopPerk(string name, int price = 0)
    {
        this.Name = name;
        this.Price = price;
    }

    public bool IsVoid => string.IsNullOrEmpty(Name);
}