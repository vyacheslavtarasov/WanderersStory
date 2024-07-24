using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.InputSystem;




public class MainMenuWindow : AnimatedWindow
{
    private Action _afterCloseAction;

    public GameObject Session;

    public void StartGame()
    {
        _afterCloseAction = () => {
            GameObject session = Instantiate(Session, Vector3.zero, Quaternion.identity);
            session.GetComponent<GameSession>().LoadLevelWithOpening = true;
            SceneManager.LoadScene("MechanicsDemoLevel");
        };
        Close();
    }

    public void ShowSettings()
    {
        var window = Resources.Load<GameObject>("SettingsMenuWindow");
        var canvas = FindObjectOfType<Canvas>();
        GameObject optionsMenu = Instantiate(window, canvas.transform);
        optionsMenu.GetComponent<AnimatedWindow>().Parent = this.gameObject;
    }

    public void LoadGame()
    {
        _afterCloseAction = () => { Debug.Log("loading a save");

            GameObject session = Instantiate(Session, Vector3.zero, Quaternion.identity);
            Debug.Log($"Session: {PlayerPrefs.GetString("session", "default")}");
            Debug.Log(GameSettings.I.Session.Value.LevelName);
            GetComponent<SceneLoader>().Load(GameSettings.I.Session.Value);
        };
        Close();
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
