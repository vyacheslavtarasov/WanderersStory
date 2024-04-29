using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;
    private GameSession _session;

    public void ReloadLevel()
    {
        _afterCloseAction = () => {
            _session = FindObjectOfType<GameSession>();
            _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();
            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);

        };
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
