using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LanguageSetupWidget : MonoBehaviour
{
    [SerializeField] private Dropdown _dropdown;

    private StringPersistentProperty _model;

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        _model = GameSettings.I.Locale;
        _model.OnChanged += OnValueChanged;
        /*var a = _dropdown.options.ToArray();
        Debug.Log(a[1].text);*/
        OnValueChanged(_model.Value, _model.Value);
    }
    private void OnDropdownValueChanged(int newValue)
    {
        _model.Value = _dropdown.options[newValue].text;
    }


    private void OnValueChanged(string newValue, string oldValue)
    {
        // _dropdown.SetValueWithoutNotify(1);
        _dropdown.value = _dropdown.options.FindIndex(option => option.text == newValue);
    }

    

    private void OnDestroy()
    {
        _dropdown.onValueChanged.RemoveAllListeners();
        _model.OnChanged -= OnValueChanged;
    }
}
