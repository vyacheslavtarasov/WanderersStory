using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkWidget : MonoBehaviour
{
    [SerializeField] private GameObject _active;
    [SerializeField] private Button _button;

    public delegate void OnButtonPressedWithName(PerkShopPerk _perkShopPerk);
    public event OnButtonPressedWithName OnChanged;

    private PerkShopPerk _perkItem;

    public void SetSelected()
    {
        _active.SetActive(true);
        _button.Select();
    }

    public void SetData(PerkShopPerk perkItem, bool active)
    {
        _perkItem = perkItem;
        var itemDefinition = DefsFacade.I.Perks.Get(perkItem.Name);
        _button.image.sprite = itemDefinition.Icon;
        _active.SetActive(active);
    }

    public void RemoveSubscriptions()
    {
        OnChanged = null;
    }

    public void OnButtonPressed()
    {
        Debug.Log(OnChanged.GetInvocationList().Length);
        OnChanged?.Invoke(_perkItem);
    }
}
