using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MainMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;

    public void StartGame()
    {
        _afterCloseAction = () => { SceneManager.LoadScene("Level_1"); };
        Close();
    }

    public void ShowSettings()
    {
        var window = Resources.Load<GameObject>("SettingsMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        Instantiate(window, canvas.transform);
    }

    public void QuitGame()
    {
        _afterCloseAction = () => { Application.Quit(); };
        Close();
    }

    public override void OnCloseAnimationComplete()
    {
        _afterCloseAction?.Invoke();
        base.OnCloseAnimationComplete();
        
    }

}
