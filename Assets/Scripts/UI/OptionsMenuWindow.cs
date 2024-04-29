using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OptionsMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;

    [SerializeField] private AudioSettingsWidget _music;
    [SerializeField] private AudioSettingsWidget _sound;

    protected override void Start()
    {
        base.Start();
        _music.SetModel(GameSettings.I.Music);
        _sound.SetModel(GameSettings.I.Sound);
    }

    public void CloseMenu()
    {
        Close();
    }

    public override void OnCloseAnimationComplete()
    {
        _afterCloseAction?.Invoke();
        base.OnCloseAnimationComplete();

    }

}
