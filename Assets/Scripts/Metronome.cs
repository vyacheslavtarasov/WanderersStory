using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Metronome : MonoBehaviour
{
    public bool _randomInterval = false;

    [HideIf("_randomInterval")]
    [SerializeField] private float _interval;

    [ShowIf("_randomInterval")]
    [SerializeField] private float _minValue = 0.0f;
    [ShowIf("_randomInterval")]
    [SerializeField] private float _maxValue = 0.0f;

    private float _currentValue = 0;
    private int _ticksAmount = 0;
    public UnityEvent Tick;
    public UnityEvent OnEnableMetronome;

    

    public void SetInterval(float val)
    {
        _interval = val;
        Reset();
    }

    public int GetTicksAmount()
    {
        return _ticksAmount;
    }

    public void Reset()
    {
        _currentValue = 0;
        _ticksAmount = 0;
    }

    private void FixedUpdate()
    {
        _currentValue += 0.02f;

        if (_currentValue >= _interval) 
        {
            _currentValue = 0.0f;
            _ticksAmount += 1;
            Tick?.Invoke();
        }
    }

    private void OnEnable()
    {
        if (_minValue < _maxValue && _randomInterval)
        {
            _interval = Random.Range(_minValue, _maxValue);
        }
        OnEnableMetronome?.Invoke();
        Reset();
    }

}
