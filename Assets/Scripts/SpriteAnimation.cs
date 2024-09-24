using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class FrameEvent
{
    public int FrameNumber;
    public UnityEvent OnFrame;
}

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private bool _loop = true;
    [SerializeField] private int _fps = 10;
    public UnityEvent OnFinish;
    public UnityEvent OnFinishByDisabling;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public UnityEvent OnStart;

    [SerializeField] public List<FrameEvent> OnFrame;

    private int currentSpriteIndex = 0;
    private float _seconds4Frame;
    private float _time2NextFrame;

    public void SetLoop(bool loop)
    {
        _loop = loop;
    }

    private void OnEnable()
    {
        _seconds4Frame = 1.0f / _fps;
        OnStart?.Invoke();
    }

    private void OnDisable()
    {
        // OnFinish?.Invoke();
        OnFinishByDisabling?.Invoke();
    }

    public void FlipX(bool Flip)
    {
        _spriteRenderer.flipX = Flip;
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        _time2NextFrame = _seconds4Frame;
    }

    void Update()
    {
        _time2NextFrame -= Time.deltaTime;
        if (_time2NextFrame < 0)
        {
            _spriteRenderer.sprite = this.Sprites[currentSpriteIndex];
            _time2NextFrame = _seconds4Frame;
            currentSpriteIndex += 1;
            foreach (FrameEvent frameEvent in OnFrame)
            {
                if (frameEvent.FrameNumber == currentSpriteIndex)
                {
                    frameEvent.OnFrame?.Invoke();
                }
            }
            currentSpriteIndex %= Sprites.Length;
            if (!_loop && currentSpriteIndex == 0)
            {
                this.OnFinish.Invoke();
                
                this.enabled = false;
            }
        }

    }
}
