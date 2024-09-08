using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Newtonsoft.Json;

public class GameSession : MonoBehaviour
{
    public PlayerData Data;

    public PlayerData PlayerDataSavedAtSceneStart; // or at checkpoint

    public bool LoadLevelWithOpening = false;

    public bool Level1_Gate_Found = false;

    // [SerializeField] public string _currentCheckpointName;
    public string DefaultCheckpoint = "default";

    public delegate void OnCheckpointChanged(string newValue);
    public event OnCheckpointChanged OnChanged;

    public bool IsChecked(string checkpointName)
    {
        if(checkpointName == Data.CheckpointName)
        {
            return true;
        }
        return false;
    }

    public void SetCurrentCheckpoint(string checkpointName)
    {
        // _currentCheckpointName = checkpointName;
        Data.CheckpointName = checkpointName;
        PlayerDataSavedAtSceneStart = Data.ShallowCopy();
        OnChanged?.Invoke(checkpointName);
    }


    public void SpawnHero(string checkpointName)
    {
        var checkpoints = FindObjectsOfType<Checkpoint>();
        foreach (var checkpoint in checkpoints)
        {
            if(checkpoint.Name == checkpointName)
            {
                checkpoint.SpawnHero();
                return;
            }
        }
        foreach (var checkpoint in checkpoints)
        {
            if (checkpoint.Name == DefaultCheckpoint)
            {
                checkpoint.SpawnHero();
                return;
            }
        }
    }

    

    private void Awake()
    {
        SceneManager.LoadScene("HUD", LoadSceneMode.Additive);

        GameSession existGameSession = GetExistSession();

        if (existGameSession != null) // we are on a new level or reload, in other session awake. we need to get our previous session to spawn a hero.
        {
            SpawnHero(existGameSession.Data.CheckpointName);
            PlayerDataSavedAtSceneStart = Data.ShallowCopy();
            DestroyImmediate(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            SpawnHero(DefaultCheckpoint);
        }       
    }

    private GameSession GetExistSession()
    {
        var sessions = FindObjectsOfType<GameSession>();

        foreach (var gameSession in sessions)
        {
            if (gameSession != this)
            {
                return gameSession;
            }
        }
        return null;
    }
}
