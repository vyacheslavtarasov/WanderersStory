using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioSettingsController : MonoBehaviour
{

    [SerializeField] private string _controlMode;
    [SerializeField] private AudioSource _source;
    private FloatPersistenProperty _model;
    // Start is called before the first frame update
    void Start()
    {
        _model = GetProperty();
        _model.OnChanged += OnSoundSettingsChanged;
        OnSoundSettingsChanged(_model.Value, _model.Value);
    }

    private void OnSoundSettingsChanged(float newValue, float oldValue)
    {
        if (_source != null)
        {
            _source.volume = newValue;
        }
        
    }

    private FloatPersistenProperty GetProperty()
    {
        switch (_controlMode)
        {
            case "Music":
                return GameSettings.I.Music;
            case "Sound":
                return GameSettings.I.Sound;
        }

        throw new ArgumentException("unknown mode");
    }
}
