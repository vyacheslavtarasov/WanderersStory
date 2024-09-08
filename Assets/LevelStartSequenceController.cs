using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

        foreach (ObjectState state in _session.Data.LevelObjectsState)
        {
            GameObject obj = GameObject.Find(state.ObjectName);
            obj.GetComponent<StatefulObject>().SetCurrentState(state.CurrentState);
            obj.GetComponent<StatefulObject>().ApplyCurrentState();
        }

        if (!_session.LoadLevelWithOpening) return;

        _session.LoadLevelWithOpening = false;
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
