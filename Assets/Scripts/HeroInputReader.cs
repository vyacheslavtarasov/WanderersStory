using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] Hero _hero;


    public void CloseAllAnimatedWindows(InputAction.CallbackContext context)
    {
        foreach (AnimatedWindow obj in FindObjectsOfType<AnimatedWindow>())
        {
            obj.Close();
        }
    }

    public void SetDirection(InputAction.CallbackContext context)
    {

        Vector3 vec = Vector3.zero;


        if (context.ReadValue<float>() == -1.0f)
        {
            vec = Vector3.left;
        }

        if (context.ReadValue<float>() == 1.0f)
        {
            vec = Vector3.right;
        }

        _hero.SetDirection(vec);
    }

    public void SwipeQuickInventory(InputAction.CallbackContext context)
    {

        bool direction = false;

        if (context.started)
        {
            if (context.ReadValue<float>() == -1.0f)
            {
                direction = false;
            }

            if (context.ReadValue<float>() == 1.0f)
            {
                direction = true;
            }

            _hero.ChangeQuickMenuFocus(direction);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.SetJumping(true);
        }
        if (context.canceled)
        {
            _hero.SetJumping(false);
        }


    }

    public void Up(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.SetUp(true);
        }
        if (context.canceled)
        {
            _hero.SetUp(false);
        }


    }

    public void JumpFromShelf(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            _hero.JumpFromShelf();
        }

    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.Interact();
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.Attack();
        }
    }

    public void SpellCast(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.SpellCast();
        }
    }

    public void CallPauseMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _hero.CallPauseMenu();
        }
    }
}
