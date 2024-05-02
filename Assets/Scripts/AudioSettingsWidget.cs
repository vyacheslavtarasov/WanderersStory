using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsWidget : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    private FloatPersistenProperty _model;

    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    public void SetModel(FloatPersistenProperty model)
    {
        _model = model;
        model.OnChanged += OnValueChanged;
        OnValueChanged(model.Value, model.Value);
    }

    private void OnValueChanged(float newValue, float oldValue)
    {
        var textValue = Mathf.Round(newValue * 100);
        _text.text = textValue.ToString();
        _slider.SetValueWithoutNotify(newValue);
    }

    private void OnSliderValueChanged(float newValue)
    {
        _model.Value = newValue;
    }

    private void OnDestroy()
    {
        _slider.onValueChanged.RemoveAllListeners();
        _model.OnChanged -= OnValueChanged;
    }


}
