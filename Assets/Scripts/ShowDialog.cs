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
    public bool Enabled { get => enabled; set => enabled = value; }


    public void Show()
    {
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
        _dialogBox.ShowDialog(_dialogEntry.Data);
        float randomFloat = Random.value;
        _dialogBox.AddListener(randomFloat.ToString(), DialogFinished);
    }

    public void DialogFinished()
    {
        DialogFinishedEvent?.Invoke();
        return;
    }


    public void Show(DialogEntry dialogEntry)
    {
        if (Skip)
        {
            DialogFinishedEvent?.Invoke();
            return;
        }
        if (!Enabled) return;
        _dialogBox = FindObjectOfType<DialogBoxController>();
        
        if (_dialogBox._dialogActive) return;
        _dialogBox.ShowDialog(dialogEntry.Data);
        float randomFloat = Random.value;
        _dialogBox.AddListener(randomFloat.ToString(), DialogFinished);
    }
}
