using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LevelStartSequenceController : MonoBehaviour
{
    [SerializeField] private DialogEntry _levelName;
    [SerializeField] private DialogEntry _dialog;
    private GameSession _session;

    LevelTitleController _levelTitleController;

    public UnityEvent LevelTitleFinishedEvent;

    public string LocalizationLanguage = "";
    private StringPersistentProperty _model;



    private void OnValueChanged(string newValue, string oldValue)
    {
        LocalizationLanguage = newValue;
    }

    public void DialogFinished()
    {
        LevelTitleFinishedEvent?.Invoke();
        return;
    }

    private void Start()
    {
        _session = FindObjectOfType<GameSession>();

        _model = GameSettings.I.Locale;
        _model.OnChanged += OnValueChanged;
        OnValueChanged(_model.Value, _model.Value);

        Debug.Log("applying object states");

        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
        foreach (Transform obj in objs)
        {
            foreach (ObjectState state in _session.Data.LevelObjectsState)
            {
                if (obj != null && state.ObjectName == obj.name)
                {
                    
                    StatefulObject component = obj.GetComponent<StatefulObject>();
                    if (component != null)
                    {
                        component.SetCurrentState(state.CurrentState);
                        component.GetComponent<StatefulObject>().ApplyCurrentState();
                    }
                    else
                    {
                        Debug.Log(obj.name);
                    }
                }
            }
        }




        /*foreach (ObjectState state in _session.Data.LevelObjectsState)
        {
            GameObject obj = GameObject.Find(state.ObjectName);
            if (obj != null)
            {
                obj.GetComponent<StatefulObject>().SetCurrentState(state.CurrentState);
                obj.GetComponent<StatefulObject>().ApplyCurrentState();
            }
            else
            {
                Debug.Log(state.ObjectName);
            }
            
        }*/

        /*GameObject FindInActiveObjectByName(string name)
        {
            Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>();
            foreach (Transform obj in objs)
            {
                if (obj.hideFlags == HideFlags.None && obj.name == name)
                {
                    return obj.gameObject;
                }
            }
            return null;
        }*/

        InputActionAsset InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "ArcadeLevelDefault")
            {
                Debug.Log("enabling arcade hero controller3");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }

        if (!_session.LoadLevelWithOpening) return;

        _session.LoadLevelWithOpening = false;
        Debug.Log("setting LoadLevelWithOpening to false");
        

        if (_dialog != null)
        {
            GetComponent<ShowDialog>().Show(_dialog);
        }
        else
        {
            Show(_levelName);
        }

        
    }


    public void Show(DialogEntry dialogEntry)
    {
        _levelTitleController = FindObjectOfType<LevelTitleController>();
        _levelTitleController.ShowTitle(dialogEntry.GetLocalizedData(LocalizationLanguage));
        _levelTitleController.TitleShowFinishedEvent.AddListener(DialogFinished);
    }
}
