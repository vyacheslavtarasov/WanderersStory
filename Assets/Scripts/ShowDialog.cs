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

    public UnityEvent DialogFinishedEvent;

    public void Show()
    {
        if(_dialogBox == null)
        {
            _dialogBox = FindObjectOfType<DialogBoxController>();           
        }
        if (_dialogBox._dialogActive) return;
        _dialogBox.DialogFinishedEvent.AddListener(DialogFinished);
    }

    public void DialogFinished()
    {
        DialogFinishedEvent?.Invoke();
        return;
    }


    public void Show(DialogEntry dialogEntry)
    {

        _dialogBox = FindObjectOfType<DialogBoxController>();
        if (_dialogBox._dialogActive) return;
        _dialogBox.ShowDialog(dialogEntry.Data);
        _dialogBox.DialogFinishedEvent.AddListener(DialogFinished);
    }
}
