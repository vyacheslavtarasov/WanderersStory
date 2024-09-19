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

            var sessions = FindObjectsOfType<GameSession>();

            foreach (var gameSession in sessions)
            {
                Destroy(gameSession.gameObject);
            }

            GameObject session = Instantiate(Session, Vector3.zero, Quaternion.identity);
            session.GetComponent<GameSession>().LoadLevelWithOpening = true;
            session.GetComponent<GameSession>().PlayerDataSavedAtSceneStart  = session.GetComponent<GameSession>().Data.ShallowCopy();
            Debug.Log(session.GetComponent<GameSession>().LoadLevelWithOpening);
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
