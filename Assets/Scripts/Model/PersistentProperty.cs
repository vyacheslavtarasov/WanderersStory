using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PersistentProperty<TPropertyType>
{
    [SerializeField] private TPropertyType _value;
    private TPropertyType _stored;

    private TPropertyType _defaultValue;
    private string _key;

    public delegate void OnPropertyChanged(TPropertyType newValue, TPropertyType oldValue);
    public event OnPropertyChanged OnChanged;

    public PersistentProperty(string key, TPropertyType defaultValue)
    {
        _defaultValue = defaultValue;
        _key = key;
    }

    public TPropertyType Value
    {
        get => _stored;
        set
        {
            var isEquals = _stored.Equals(value);
            if (isEquals) return;

            var oldValue = _value;
            Write(_key, value);
            _stored = _value = value;

            OnChanged?.Invoke(value, oldValue);
        }
    }

    protected void Init()
    {
        _stored =_value = Read(_key, _defaultValue);
    }

    public void Validate()
    {
        if (!_stored.Equals(_value))
        {
            Value = _value;
        }
    }

    protected abstract void Write(string key, TPropertyType value);
    protected abstract TPropertyType Read(string key, TPropertyType defaultValue);
}
