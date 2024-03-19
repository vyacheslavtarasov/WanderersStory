using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroInputReader : MonoBehaviour
{
    [SerializeField] Hero _hero;


    public void SetDirection(InputAction.CallbackContext context)
    {
        // Debug.Log("here");
        Debug.Log(context.ReadValue<float>());
        Debug.Log(context.phase);

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

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("jump");
        Debug.Log(context.ReadValue<float>());
        Debug.Log(context.phase);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
