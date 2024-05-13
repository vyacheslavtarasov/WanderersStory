using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkShopController : MonoBehaviour
{
    
    [SerializeField] private List<PerkShopPerk> _availablePerks; // Model
    private PerkShopView _perkShopView;
    // Start is called before the first frame update

    public List<PerkShopPerk> AvailablePerks => _availablePerks;
    void Start()
    {
        _perkShopView = FindObjectOfType<PerkShopView>(true);
        OnChanged += _perkShopView.Redraw;
        
    }

    public delegate void OnModelChanged(List<PerkShopPerk> _perks);

    public OnModelChanged OnChanged;

    public void ShowUI()
    {
        _perkShopView.SetController(this);
        if (_perkShopView.gameObject.active)
        {
            _perkShopView.gameObject.SetActive(false);
        }
        else
        {
            _perkShopView.gameObject.SetActive(true);
        }
    }
    
    public void Remove(string name)
    {
        var perkDef = DefsFacade.I.Perks.Get(name);
        if (perkDef.IsVoid) return;

        var item = GetPerk(name);
        if (item.IsVoid) return;

        _availablePerks.Remove(item);

        OnChanged?.Invoke(_availablePerks);
    }

    private PerkShopPerk GetPerk(string name)
    {
        foreach (var perkData in _availablePerks)
        {
            if (perkData.Name == name)
                return perkData;
        }

        return new PerkShopPerk(null);
    }
}
