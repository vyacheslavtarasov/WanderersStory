using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnimatedWindow : MonoBehaviour
{

    protected Animator _animator;

    public InputActionAsset InputActionAsset;

    public GameObject DefaultButton;

    public GameObject Parent;

    protected SoundPlayer _soundPlayer4OneShots;
    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");
    }


    protected virtual void OnEnable()
    {
        _animator.SetTrigger("show");
        _soundPlayer4OneShots = FindObjectOfType<Camera>().gameObject.GetComponent<SoundPlayer>();
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "UI")
            {
                Debug.Log("enabling UI animated window controller4");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }

        if (DefaultButton != null)
        {
            DefaultButton.GetComponent<Button>().Select();
        }

       

        Button[] allButtons1 = GetComponentsInChildren<Button>();

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

            button.onClick.AddListener(DefaultButtonClick);
        }

        Dropdown[] allDropdowns = GetComponentsInChildren<Dropdown>();

        foreach (Dropdown dropdown in allDropdowns)
        {
            // Add EventTrigger component to the Button
            EventTrigger trigger = dropdown.gameObject.AddComponent<EventTrigger>();

            // Create a new entry for the OnSelect event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;

            // Subscribe the method to the event
            entry.callback.AddListener((eventData) => { OnButtonSelected(); });

            // Add the entry to the EventTrigger
            trigger.triggers.Add(entry);

            dropdown.onValueChanged.AddListener(DefaultButtonClick);
        }


        Slider[] allSliders = GetComponentsInChildren<Slider>();

        foreach (Slider slider in allSliders)
        {
            // Add EventTrigger component to the Button
            EventTrigger trigger = slider.gameObject.AddComponent<EventTrigger>();

            // Create a new entry for the OnSelect event
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;

            // Subscribe the method to the event
            entry.callback.AddListener((eventData) => { OnButtonSelected(); });

            // Add the entry to the EventTrigger
            trigger.triggers.Add(entry);

            slider.onValueChanged.AddListener(DefaultButtonClick);
        }


    }

    public void DefaultButtonClick(float a)
    {
        _soundPlayer4OneShots.Play("Click");
    }

    public void DefaultButtonClick(int a)
    {
        _soundPlayer4OneShots.Play("Click");
    }

    public void DefaultButtonClick()
    {
        _soundPlayer4OneShots.Play("Click");
    }
    public void OnButtonSelected()
    {
        // Debug.Log("Button Selected!");
        // Debug.Log(_soundPlayer4OneShots);
        _soundPlayer4OneShots.Play("Spring");
        // Your custom logic here
    }

    public void Close()
    {
        Debug.Log("calling close");
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "ArcadeLevelDefault")
            {
                Debug.Log("enabling arcade animated window controller5");
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }
        _animator.SetTrigger("hide");
        _soundPlayer4OneShots.Play("Swoosh");
    }

    public virtual void OnCloseAnimationComplete()
    {
        Destroy(gameObject);
    }



}

