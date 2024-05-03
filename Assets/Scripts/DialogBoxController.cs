using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _container;
    [SerializeField] private Animator _animator;

    [Space]
    [SerializeField] private float _textSpeed = 0.09f;

    [SerializeField] private AudioClip _typing;
    [SerializeField] private AudioClip _open;
    [SerializeField] private AudioClip _close;


    private DialogData _dialogData;
    private Coroutine _typingRoutine;
    private int _currentSentence = 0;
    private bool _sentenceTypeComplete = false;
    public void ShowDialog(DialogData data)
    {
        _dialogData = data;
        _currentSentence = 0;
        _text.text = string.Empty;

        _container.SetActive(true);
        _animator.SetBool("show", true);
    }

    private void OnShowAnimationComplete()
    {
        _typingRoutine = StartCoroutine(TypeSentence());
    }

    public void OnNextButtonClick()
    {
        var sentence = _dialogData.Sentences[_currentSentence];

        if (!_sentenceTypeComplete)
        {
            StopCoroutine(_typingRoutine);
            _text.text = sentence;
            _sentenceTypeComplete = true;
            return;
        }
        else
        {
            _currentSentence += 1;
        }

        if(_currentSentence >= _dialogData.Sentences.Length)
        {
            _animator.SetBool("show", false);
        }
        else
        {
            _typingRoutine = StartCoroutine(TypeSentence());
        }
    }

    private void OnHideAnimationComplete()
    {
        
    }

    private IEnumerator TypeSentence()
    {
        _sentenceTypeComplete = false;
        _text.text = string.Empty;
        var sentence = _dialogData.Sentences[_currentSentence];
        foreach (var letter  in sentence)
        {
            _text.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
        _sentenceTypeComplete = true;
    }

    [SerializeField] private DialogData _testData;
    public void test()
    {
        ShowDialog(_testData);
    }

}
