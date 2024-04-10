using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string _levelName;

    private GameSession _session;

    public void Load()
    {
        _session = FindObjectOfType<GameSession>();
        _session.PlayerDataSavedAtSceneStart = _session.Data.ShallowCopy();
        SceneManager.LoadScene(_levelName);
    }
}