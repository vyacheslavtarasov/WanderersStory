using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeadSplashScreenController : AnimatedWindow
{
    private GameSession _session;
    private Hero hero;
    private bool retry = false;

    public override void Awake()
    {
        base.Awake();
        
        _session = FindObjectOfType<GameSession>();
    }



    protected override void OnEnable()
    {
        base.OnEnable();

        /*_animator.SetTrigger("show");
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "UI")
            {
                Debug.Log("enabling UI animated window controller");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }
        
        DefaultButton.GetComponent<Button>().Select();*/

    }

    public void Retry()
    {
        retry = true;
        hero = FindObjectOfType<Hero>();
        Destroy(hero.gameObject);
        // _session = FindObjectOfType<GameSession>();
        // _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();

        _session.Data.Health = 50.0f;
        _session.SpawnHero(_session.Data.CheckpointName);



        // GetComponent<SceneLoader>().Load(GameSettings.I.Session.Value);

        Close();
    }

    public override void OnCloseAnimationComplete()
    {
        if (!retry)
        {

            // SceneManager.UnloadScene("HUD");
            _session.Data = _session.PlayerDataSavedAtSceneStart.ShallowCopy();
            SceneManager.LoadScene("MainMenu");
            retry = false;
            hero = FindObjectOfType<Hero>();
            Destroy(hero.gameObject);
            return;
        }
        gameObject.SetActive(false);
        retry = false;
        // base.OnCloseAnimationComplete();
    }

    private void OnDisable()
    {
        _animator.SetTrigger("hide");
    }
}
