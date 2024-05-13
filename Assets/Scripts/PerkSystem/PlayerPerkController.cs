using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPerkController : MonoBehaviour
{
    private GameSession _session;
    private List<PlayerPerk> _perks;
    // private PerkShopView _perkShopView;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        _perks = _session.Data.Perks;
        // OnChanged += _perkShopView.Redraw;
    }

    public delegate void OnInventoryChanged(List<PlayerPerk> _perks);

    public OnInventoryChanged OnChanged;

    public void Add(string name)
    {
        var perkDef = DefsFacade.I.Perks.Get(name);
        if (perkDef.IsVoid) return;

        PlayerPerk perk;
        int perkIndex = GetPerkIndex(name);
        if (perkIndex < 0)
        {
            perk = new PlayerPerk(name);
            _perks.Add(perk);
        }
        else
        {
            return;
        }

        OnChanged?.Invoke(_perks);
    }

    public void ActivatePerk(string name)
    {
        for (int i = 0; i < _perks.Count; i++)
        {
            if (_perks[i].Name == name)
            {
                Debug.Log("activating");
                _perks[i] = new PlayerPerk(name, true, -1);
            }
            else if (_perks[i].Active)
            {
                Debug.Log("deactivating");
                _perks[i] = new PlayerPerk(_perks[i].Name, false, -1);

            }
        }


        OnChanged?.Invoke(_perks);
    }

    public void SetDirty()
    {
        OnChanged?.Invoke(_perks);
    }

    public void Remove(string name)
    {
        var perkDef = DefsFacade.I.Perks.Get(name);
        if (perkDef.IsVoid) return;

        var item = GetItem(name);
        if (item.IsVoid) return;

        _perks.Remove(item);

        OnChanged?.Invoke(_perks);
    }

    public PlayerPerk GetItem(string name)
    {
        foreach (var perkData in _perks)
        {
            if (perkData.Name == name)
                return perkData;
        }

        return new PlayerPerk(null);
    }

    private int GetPerkIndex(string name)
    {
        for (var i = 0; i < _perks.Count; i++)
        {
            if (_perks[i].Name == name)
                return i;
        }
        return -1;
    }
}
