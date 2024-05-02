using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemWidget : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _selection;
    [SerializeField] private Text _amount;

    public void SetSelected()
    {
        _selection.SetActive(true);
    }

    public void SetData(InventoryItemData item)
    {
        var itemDefinition = DefsFacade.I.Items.Get(item.Name);
        _icon.sprite = itemDefinition.Icon;
        _amount.text = item.Amount.ToString();

    }
}
