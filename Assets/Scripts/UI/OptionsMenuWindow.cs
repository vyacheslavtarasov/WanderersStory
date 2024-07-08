using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class OptionsMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;

    [SerializeField] private AudioSettingsWidget _music;
    [SerializeField] private AudioSettingsWidget _sound;

    protected override void OnEnable()
    {
        base.OnEnable();
        _music.SetModel(GameSettings.I.Music);
        _sound.SetModel(GameSettings.I.Sound);
    }

    public void CloseMenu()
    {
        if (Parent != null && Parent.GetComponent<AnimatedWindow>() != null)
        {
            _animator.SetTrigger("hide");
            Parent.GetComponent<AnimatedWindow>().DefaultButton.GetComponent<Button>().Select();
        }
        else
        {
            Close();
        }
    }

    public override void OnCloseAnimationComplete()
    {
        _afterCloseAction?.Invoke();
        
        base.OnCloseAnimationComplete();

    }

}
