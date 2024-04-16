using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Metronome : MonoBehaviour
{
    [SerializeField] private float _interval;
    private float _currentValue = 0;
    private int _ticksAmount = 0;
    public UnityEvent Tick;

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
        Reset();
    }

}
