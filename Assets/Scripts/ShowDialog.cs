using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShowDialog : MonoBehaviour
{
    [SerializeField] private DialogEntry _dialogEntry;
    DialogBoxController _dialogBox;
    public bool Skip { get; set; }

    public UnityEvent DialogFinishedEvent;
    public UnityEvent YesEvent;
    public UnityEvent NoEvent;

    public InteractionEvent DialogFinishedEventWithInitiator;
    public InteractionEvent YesEventWithInitiator;
    public InteractionEvent NoEventWithInitiator;

    public GameObject Initiator;

    private StringPersistentProperty _model;

    public string LocalizationLanguage = "";
    public bool Enabled { get => enabled; set => enabled = value; }

    public bool IsQuestion = false;

    private void Awake()
    {
        _model = GameSettings.I.Locale;
        LocalizationLanguage = _model.Value.ToString();
        Debug.Log(_model.Value.ToString());
        Debug.Log(LocalizationLanguage);
        _model.OnChanged += OnValueChanged;
        OnValueChanged(_model.Value, _model.Value);
    }

    private void OnValueChanged(string newValue, string oldValue)
    {
        LocalizationLanguage = newValue;
        // Debug.Log(LocalizationLanguage);
    }

    public void Show()
    {
        // Debug.Log(LocalizationLanguage);



        if (Skip)
        {
            DialogFinishedEvent?.Invoke();
            return;
        }
        if (!Enabled) return;
        if(_dialogBox == null)
        {
            _dialogBox = FindObjectOfType<DialogBoxController>();
        }
        if (_dialogBox._dialogActive) return;
        float randomFloat = Random.value;
        _dialogBox.AddListener(randomFloat.ToString(), DialogFinished);
        _dialogBox.AddYesListener(randomFloat.ToString(), YesChosen);
        _dialogBox.AddNoListener(randomFloat.ToString(), NoChosen);
        _dialogBox.ShowDialog(_dialogEntry.GetLocalizedData(LocalizationLanguage), IsQuestion);
    }

    public void Show(GameObject initiator, GameObject source)
    {
        Debug.Log(LocalizationLanguage);

        Initiator = initiator;

        if (Skip)
        {
            DialogFinishedEvent?.Invoke();
            return;
        }
        if (!Enabled) return;
        if (_dialogBox == null)
        {
            _dialogBox = FindObjectOfType<DialogBoxController>();
        }
        if (_dialogBox._dialogActive) return;
        _dialogBox.ShowDialog(_dialogEntry.GetLocalizedData(LocalizationLanguage), IsQuestion);
        float randomFloat = Random.value;
        _dialogBox.AddListener(randomFloat.ToString(), DialogFinished);
        _dialogBox.AddYesListener(randomFloat.ToString(), YesChosen);
        _dialogBox.AddNoListener(randomFloat.ToString(), NoChosen);
    }

    public void DialogFinished()
    {
        DialogFinishedEvent?.Invoke();
        DialogFinishedEventWithInitiator?.Invoke(Initiator);
        return;
    }

    public void YesChosen()
    {
        YesEvent?.Invoke();
        YesEventWithInitiator?.Invoke(Initiator);
        return;
    }

    public void NoChosen()
    {
        NoEvent?.Invoke();
        NoEventWithInitiator?.Invoke(Initiator);
        return;
    }

    public void Show(DialogEntry dialogEntry)
    {
        Debug.Log(LocalizationLanguage);
        if (Skip)
        {
            DialogFinishedEvent?.Invoke();
            return;
        }
        if (!Enabled) return;
        _dialogBox = FindObjectOfType<DialogBoxController>();
        
        if (_dialogBox._dialogActive) return;
        _dialogBox.ShowDialog(_dialogEntry.GetLocalizedData(LocalizationLanguage), IsQuestion);
        float randomFloat = Random.value;
        _dialogBox.AddListener(randomFloat.ToString(), DialogFinished);
        _dialogBox.AddYesListener(randomFloat.ToString(), YesChosen);
        _dialogBox.AddNoListener(randomFloat.ToString(), NoChosen);
    }
}
