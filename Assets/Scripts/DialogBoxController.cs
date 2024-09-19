using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.EventSystems;

public class DialogBoxController : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _container;
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _lSpeakerName;
    [SerializeField] private Text _rSpeakerName;
    [SerializeField] private SoundPlayer _soundPlayer;

    [SerializeField] private GameObject _lContainer;
    [SerializeField] private GameObject _rContainer;

    [SerializeField] private GameObject _yesButton;
    [SerializeField] private GameObject _noButton;

    private SoundPlayer _soundPlayer4OneShots;

    private bool _questionMode = false;

    [Space]
    [SerializeField] private float _textSpeed = 0.09f;

    [SerializeField] private AudioClip _typing;
    [SerializeField] private AudioClip _open;
    [SerializeField] private AudioClip _close;

    [SerializeField] public Button _nextButton;

    public bool _dialogActive = false;
    public bool yesClicked = false;
    public bool noClicked = false;

    private Dictionary<string, UnityAction> listeners = new Dictionary<string, UnityAction>();
    private Dictionary<string, UnityAction> yesListeners = new Dictionary<string, UnityAction>();
    private Dictionary<string, UnityAction> noListeners = new Dictionary<string, UnityAction>();



    public UnityEvent DialogFinishedEvent;
    public UnityEvent YesEvent;
    public UnityEvent NoEvent;

    private DialogData _dialogData;
    private Coroutine _typingRoutine;
    private int _currentSentence = 0;
    private bool _sentenceTypeComplete = false;
    private bool _isQuestion;

    public InputActionAsset InputActionAsset;

    private void Awake()
    {
        _yesButton.SetActive(false);
        _noButton.SetActive(false);
        InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");
        _soundPlayer = GetComponent<SoundPlayer>();
        _soundPlayer4OneShots = FindObjectOfType<Camera>().gameObject.GetComponent<SoundPlayer>();
        // Debug.Log("dialog box controller awake");
    }
    public void ShowDialog(DialogData data, bool IsQuestion)
    {
        
        _dialogData = data;
        _currentSentence = 0;
        _sentenceTypeComplete = false;
        _text.text = string.Empty;
        _isQuestion = IsQuestion;

        _animator.SetBool("show", true);
        _nextButton.interactable = true;

        if (_dialogData.Place == place.left)
        {
            _lSpeakerName.text = _dialogData.SpeakerName;
            if (_dialogData.SpeakerColor != Color.clear)
            {
                _lContainer.GetComponent<Image>().color = _dialogData.SpeakerColor;
            }
           
            _lContainer.SetActive(true);
            _rContainer.SetActive(false);
        }
        else
        {
            _rSpeakerName.text = _dialogData.SpeakerName;
            if (_dialogData.SpeakerColor != Color.clear)
            {
                _rContainer.GetComponent<Image>().color = _dialogData.SpeakerColor;
            }
            _lContainer.SetActive(false);
            _rContainer.SetActive(true);
        }

        _container.GetComponent<Image>().CrossFadeAlpha(1.0f, 1.0f, true);

        if (_dialogData.Place == place.none)
        {

            _lContainer.SetActive(false);
            _rContainer.SetActive(false);
            _container.GetComponent<Image>().CrossFadeAlpha(0.8f, 1.0f, true);
        }

        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "UI")
            {
                Debug.Log("enabling UI dialog box controller6");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }
    }

    private void OnShowAnimationComplete()
    {
        _typingRoutine = StartCoroutine(TypeSentence());
        _dialogActive = true;

        _nextButton.Select();

        Button[] allButtons1 = GetComponentsInChildren<Button>(true);

        foreach (Button button in allButtons1)
        {
            // Add EventTrigger component to the Button
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // Create a new entry for the OnSelect event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;

            // Subscribe the method to the event
            entry.callback.AddListener((eventData) => { OnButtonSelected(); });

            // Add the entry to the EventTrigger
            trigger.triggers.Add(entry);

            // button.onClick.AddListener(DefaultButtonClick);
        }
    }

    public void OnButtonSelected()
    {
        // Debug.Log("Button Selected!");
        // Debug.Log(_soundPlayer4OneShots);
        Button[] allButtons1 = GetComponentsInChildren<Button>();
        if (allButtons1.Length != 1)
            _soundPlayer4OneShots.Play("Spring");
        // Your custom logic here
    }

    public void DefaultButtonClick()
    {
        _soundPlayer4OneShots.Play("Click");
    }


    public void OnYesButtonClick()
    {
        yesClicked = true;
        DefaultButtonClick();
        _animator.SetBool("show", false);
        _dialogActive = false;


    }
    public void OnNoButtonClick()
    {
        noClicked = true;
        DefaultButtonClick();
        _animator.SetBool("show", false);
        _dialogActive = false;


    }
    public void OnNextButtonClick()
    {
        _soundPlayer.Pause();
        if (!_dialogActive) return;
        if (_questionMode)
        {
            _animator.SetBool("show", false);
            _dialogActive = false;
            _questionMode = false;
            DefaultButtonClick();
            return;
        }
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
            DefaultButtonClick();
        }

        if(_currentSentence >= _dialogData.Sentences.Length)
        {
            if (_isQuestion)
            {
                _yesButton.SetActive(true);
                _noButton.SetActive(true);
                _questionMode = true;
            }
            else
            {
                _animator.SetBool("show", false);
                _dialogActive = false;
            }
            DefaultButtonClick();
        }
        else
        {
            _typingRoutine = StartCoroutine(TypeSentence());
        }
    }


    public void AddListener(string id, UnityAction action)
    {
        if (!listeners.ContainsKey(id))
        {
            listeners.Add(id, action);
            DialogFinishedEvent.AddListener(action);
        }
    }

    public void RemoveListener(string id)
    {
        if (listeners.TryGetValue(id, out UnityAction action))
        {
            DialogFinishedEvent.RemoveListener(action);
            listeners.Remove(id);
        }
    }

    public void AddYesListener(string id, UnityAction action)
    {
        // Debug.Log("adding" + id);
        if (!yesListeners.ContainsKey(id))
        {
            yesListeners.Add(id, action);
            YesEvent.AddListener(action);
        }
    }

    public void RemoveYesListener(string id)
    {
        if (yesListeners.TryGetValue(id, out UnityAction action))
        {
            YesEvent.RemoveListener(action);
            yesListeners.Remove(id);
        }
    }

    public void AddNoListener(string id, UnityAction action)
    {
        if (!noListeners.ContainsKey(id))
        {
            noListeners.Add(id, action);
            NoEvent.AddListener(action);
        }
    }

    public void RemoveNoListener(string id)
    {
        if (noListeners.TryGetValue(id, out UnityAction action))
        {
            NoEvent.RemoveListener(action);
            noListeners.Remove(id);
        }
    }
    private void OnHideAnimationComplete()
    {
        _dialogActive = false;
        _nextButton.interactable = false;
        _yesButton.SetActive(false);
        _noButton.SetActive(false);


        Button[] allButtons1 = GetComponentsInChildren<Button>(true);

        foreach (Button button in allButtons1)
        {
            // Add EventTrigger component to the Button
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // Create a new entry for the OnSelect event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;

            // Subscribe the method to the event
            entry.callback.AddListener((eventData) => { OnButtonSelected(); });

            entry.callback.RemoveAllListeners();

            button.onClick.RemoveAllListeners();
        }




        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "ArcadeLevelDefault")
            {
                Debug.Log("enabling arcade dialog box controller7");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }

        if (noClicked)
        {
            string[] noNames = noListeners.Keys.ToArray();

            NoEvent?.Invoke(); // here you can add new listeners, so we need to remove not all, but previously existed.

            foreach (string name in noNames)
            {
                RemoveNoListener(name);
            }
        }

        if (yesClicked)
        {
            string[] yesNames = yesListeners.Keys.ToArray();

            YesEvent?.Invoke(); // here you can add new listeners, so we need to remove not all, but previously existed.

            foreach (string name in yesNames)
            {
                // Debug.Log("removing " + name);
                RemoveYesListener(name);
            }
            yesClicked = false;
        }

        string[] Names = listeners.Keys.ToArray();

        DialogFinishedEvent?.Invoke(); // here you can add new listeners, so we need to remove not all, but previously existed.
        
        foreach(string name in Names)
        {
            RemoveListener(name);
        } 

        _questionMode = false;
    }

    private IEnumerator TypeSentence()
    {
        _sentenceTypeComplete = false;
        _text.text = string.Empty;
        _soundPlayer.PlayLoop(_dialogData.SpeakerSound);
        Debug.Log(_dialogData.SpeakerSound);
        var sentence = _dialogData.Sentences[_currentSentence];
        // Debug.Log(_currentSentence);
        // Debug.Log(sentence);
        foreach (var letter  in sentence)
        {
            _text.text += letter;
            yield return new WaitForSeconds(_textSpeed);
        }
        _sentenceTypeComplete = true;
        _soundPlayer.Pause();
    }

    [SerializeField] private DialogData _testData;
    public void test()
    {
        ShowDialog(_testData, false);
    }

}
