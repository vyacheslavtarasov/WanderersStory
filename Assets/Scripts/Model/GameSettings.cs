using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistenProperty _music;
    [SerializeField] private FloatPersistenProperty _sound;
    [SerializeField] private StringPersistentProperty _locale;

    public FloatPersistenProperty Music => _music;
    public FloatPersistenProperty Sound => _sound;

    public StringPersistentProperty Locale => _locale;

    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

    private static GameSettings LoadGameSettings() 
    {
        var a = Resources.Load<GameSettings>("GameSettings");
        _instance = a;
        return Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable()
    {
        _music = new FloatPersistenProperty("music", 1.0f);
        _sound = new FloatPersistenProperty("sound", 1.0f);
        _locale = new StringPersistentProperty("locale", "English");
    }

    private void OnValidate()
    {
        try
        {
            _music.Validate();
            _sound.Validate();
            _locale.Validate();
        }
        catch
        {
            Debug.Log("Here is the error, need ot fix it.");
        }
        
    }
}
