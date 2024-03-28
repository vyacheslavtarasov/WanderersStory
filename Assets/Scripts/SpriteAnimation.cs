using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpriteAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private bool _loop = true;
    [SerializeField] private int _fps = 10;
    public UnityEvent OnFinish;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private int currentSpriteIndex = 0;
    private float _seconds4Frame;
    private float _time2NextFrame;

    private void OnEnable()
    {
        _seconds4Frame = 1.0f / _fps;
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
            currentSpriteIndex %= Sprites.Length;
            if (!_loop && currentSpriteIndex == 0)
            {
                this.OnFinish.Invoke();
                this.enabled = false;
            }
        }

    }
}
