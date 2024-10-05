using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuExiter : MonoBehaviour
{
    public void ExitToMainMenu()
    {
        GameSession _session = FindObjectOfType<GameSession>();
        _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();// ?
        SceneManager.LoadScene("MainMenu");

    }

}
