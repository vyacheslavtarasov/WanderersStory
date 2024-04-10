using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ReloadLevel : MonoBehaviour
{

    private GameSession _session;
    public void Reload()
    {

        _session = FindObjectOfType<GameSession>();
        _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
