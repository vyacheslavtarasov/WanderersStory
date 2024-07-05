using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class LevelTitleController: MonoBehaviour
{
    [SerializeField] private Text _locactionName;
    [SerializeField] private Text _placeName;
    [SerializeField] private Animator _animator;

    [Space]
    [SerializeField] private float _textSpeed = 0.09f;
    [SerializeField] private float _waitAtTheEnd = 1.0f;

    [SerializeField] private AudioClip _typing;
    [SerializeField] private AudioClip _open;
    [SerializeField] private AudioClip _close;

    public UnityEvent TitleShowFinishedEvent;

    private DialogData _dialogData;
    private Coroutine _typingRoutine;
    private int _currentSentence = 1;

    public void ShowTitle(DialogData data)
    {

        _dialogData = data;
        Debug.Log(_dialogData.Sentences[0]);
        _locactionName.text = _dialogData.Sentences[0];
        _placeName.text = string.Empty;

        _animator.SetBool("Show", true);
    }

    private void OnShowAnimationComplete()
    {
        _typingRoutine = StartCoroutine(TypeSentence());
    }

    private void OnHideAnimationComplete()
    {

        TitleShowFinishedEvent?.Invoke();
        TitleShowFinishedEvent.RemoveAllListeners();
    }

    private IEnumerator TypeSentence()
    {
        _placeName.text = string.Empty;
        var sentence = _dialogData.Sentences[_currentSentence];

        foreach (var letter in sentence)
        {
            _placeName.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
        yield return new WaitForSeconds(_waitAtTheEnd);

        _animator.SetBool("Show", false);
    }

}
