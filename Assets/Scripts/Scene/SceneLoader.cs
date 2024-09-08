using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _levelName;

    private GameSession _session;

    public void Load()
    {
        _session = FindObjectOfType<GameSession>();
        _session.PlayerDataSavedAtSceneStart = _session.Data.ShallowCopy();
        _session.Data.CheckpointName = _session.DefaultCheckpoint;
        SceneManager.LoadScene(_levelName);
        Debug.Log("loading!");
    }

    public void Load(PlayerData data)
    {
        _session = FindObjectOfType<GameSession>();
        _session.PlayerDataSavedAtSceneStart = data;
        _session.Data = data;
        _session.Data.CheckpointName = data.CheckpointName;
        SceneManager.LoadScene(data.LevelName);
    }
}
