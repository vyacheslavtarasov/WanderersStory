using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ReloadLevel : MonoBehaviour
{

    private GameSession _session;
    private Hero hero;
    private DeadSplashScreenController _deadSplashScreenController;

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();
        hero = FindObjectOfType<Hero>();
        _deadSplashScreenController = FindObjectOfType<DeadSplashScreenController>(true);

    }
    public void Reload()
    {
        
        _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void RespawnHero()
    {
        

        // Destroy(hero.gameObject);
        hero.gameObject.GetComponent<Rigidbody2D>().simulated = false;
        


        _deadSplashScreenController.gameObject.SetActive(true);

        /*_session = FindObjectOfType<GameSession>();
        _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();
        _session.SpawnHero(_session._currentCheckpointName);*/
    }
}
