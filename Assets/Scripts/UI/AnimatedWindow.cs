using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedWindow : MonoBehaviour
{

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _animator.SetTrigger("show");
    }

    public void Close()
    {
        _animator.SetTrigger("hide");
    }

    public virtual void OnCloseAnimationComplete()
    {
        Destroy(gameObject);
    }

}
