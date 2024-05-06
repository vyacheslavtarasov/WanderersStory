using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Text))]
public class LocalizationController : MonoBehaviour
{

    [SerializeField] private string _localizationKey;
    [SerializeField] private string _localizationSource;
    [SerializeField] private string _comment;

    private StringPersistentProperty _model;
    private LocalizationData4Language _data;
    public string LocalizationKey => _localizationKey;

    private Text _textComponent;
    
    void Awake()
    {
        _textComponent = GetComponent<Text>();
    }

    private void Start()
    {
        _model = GameSettings.I.Locale;
        _model.OnChanged += OnValueChanged;
        OnValueChanged(_model.Value, _model.Value);
    }

    private void OnValueChanged(string newValue, string oldValue)
    {


        _data = LocalizationData4Language.I(newValue);

        foreach(var entry in _data.LocalizationEntriesList)
        {
            if (entry._key == _localizationKey)
            {
                _textComponent.text = entry._translation;
                break;
            }
        }
         
    }



    private void OnDestroy()
    {
        _model.OnChanged -= OnValueChanged;
    }


}
