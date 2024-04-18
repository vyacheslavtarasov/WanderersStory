using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    public PlayerData Data;

    // public PlayerData Data => _data;

    public PlayerData PlayerDataSavedAtSceneStart;

    private void Awake()
    {
        Debug.Log("session");
        if (IsSessionExist())
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }

        PlayerDataSavedAtSceneStart = Data.ShallowCopy();
    }

    private bool IsSessionExist()
    {
        var sessions = FindObjectsOfType<GameSession>();

        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return true;
            }
        }
        return false;
    }
}
