using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AnimatedWindow : MonoBehaviour
{

    protected Animator _animator;

    public InputActionAsset InputActionAsset;

    public GameObject DefaultButton;

    public GameObject Parent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        InputActionAsset = Resources.Load<InputActionAsset>("HeroInputActions");
    }


    protected virtual void OnEnable()
    {
        _animator.SetTrigger("show");
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "UI")
            {
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }

        DefaultButton.GetComponent<Button>().Select();
    }



    public void Close()
    {
        foreach (InputActionMap localActionMap in InputActionAsset.actionMaps)
        {
            if (localActionMap.name == "ArcadeLevelDefault")
            {
                localActionMap.Enable();
            }
            else
            {
                localActionMap.Disable();
            }
        }
        _animator.SetTrigger("hide");
    }

    public virtual void OnCloseAnimationComplete()
    {
        Destroy(gameObject);
    }



}

