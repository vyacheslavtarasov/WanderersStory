using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OptionsMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;

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
