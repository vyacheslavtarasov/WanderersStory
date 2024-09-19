using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWidget : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Text _text;

    public delegate void OnButtonPressedWithName(string _itemName);
    public event OnButtonPressedWithName OnSelected;

    private InventoryItemData _item;

    public void SetSelected()
    {
        _button.Select();
    }

    public void SetData(InventoryItemData item, string _localization)
    {
        _item = item;
        ItemDef itemDefinition = DefsFacade.I.Items.Get(item.Name);
        if (itemDefinition._name2Display != null)
        {
            _text.text = itemDefinition._name2Display.GetLocalizedData(_localization).Sentences[0];
        }
        else
        {
            _text.text = "set name!";
        }

        // transform.lossyScale.x = 1.0f;
        
        // _button.image.sprite = itemDefinition.Icon;
    }

    public void RemoveSubscriptions()
    {
        OnSelected = null;
    }

    public void OnButtonSelect()
    {
        // Debug.Log(OnSelected.GetInvocationList().Length);
        Debug.Log(_item.Name);
        OnSelected?.Invoke(_item.Name);
    }
}
