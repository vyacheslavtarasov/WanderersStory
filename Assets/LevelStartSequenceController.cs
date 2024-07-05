using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelStartSequenceController : MonoBehaviour
{
    [SerializeField] private DialogEntry _levelName;
    [SerializeField] private DialogEntry _dialog;

    LevelTitleController _levelTitleController;

    public UnityEvent LevelTitleFinishedEvent;

    public void DialogFinished()
    {
        LevelTitleFinishedEvent?.Invoke();
        return;
    }

    private void Start()
    {
        GetComponent<ShowDialog>().Show(_dialog);
    }


    public void Show(DialogEntry dialogEntry)
    {

        _levelTitleController = FindObjectOfType<LevelTitleController>();
        _levelTitleController.ShowTitle(dialogEntry.Data);
        _levelTitleController.TitleShowFinishedEvent.AddListener(DialogFinished);
    }
}
