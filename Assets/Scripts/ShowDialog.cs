using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialog : MonoBehaviour
{
    [SerializeField] private DialogEntry _dialogEntry;

    private DialogBoxController _dialogBox;

    public void Show()
    {
        if(_dialogBox == null)
        {
            _dialogBox = FindObjectOfType<DialogBoxController>();           
        }
        _dialogBox.ShowDialog(_dialogEntry.Data);

    }

    public void Show(DialogEntry dialogEntry)
    {
        _dialogEntry = dialogEntry;
        Show();
    }
}
