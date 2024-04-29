using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/GameSettings", fileName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] private FloatPersistenProperty _music;
    [SerializeField] private FloatPersistenProperty _sound;

    public FloatPersistenProperty Music => _music;
    public FloatPersistenProperty Sound => _sound;

    private static GameSettings _instance;
    public static GameSettings I => _instance == null ? LoadGameSettings() : _instance;

    private static GameSettings LoadGameSettings() 
    {
        return Resources.Load<GameSettings>("GameSettings");
    }

    private void OnEnable()
    {
        _music = new FloatPersistenProperty("music", 1.0f);
        _sound = new FloatPersistenProperty("sound", 1.0f);
    }

    private void OnValidate()
    {
        _music.Validate();
        _sound.Validate();
    }
}
