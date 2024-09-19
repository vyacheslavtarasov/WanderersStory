using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuExiter : MonoBehaviour
{
    public void ExitToMainMenu()
    {

        SceneManager.LoadScene("MainMenu");

    }

}
