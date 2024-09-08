using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using NaughtyAttributes;

public class TweenValue : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private string _componentName;
    
    [SerializeField] private string _parameterName;
    [SerializeField] private bool _fromCurrentValue = true;
    [HideIf("_fromCurrentValue")]
    [SerializeField] private float from = 0.0f;
    [SerializeField] private float to = 1.0f;
    [SerializeField] private float _timeInterval = 1.0f;
    private Component _component;
    private float step = 1.0f;

    private bool run = false;

    public UnityEvent TweenComplete;

    PropertyInfo field;


    private void Start()
    {
        _component = _object.GetComponent(_componentName);
        field = _component.GetType().GetProperty(_parameterName, BindingFlags.Public | BindingFlags.Instance);
        var a = field.GetValue(_component);
    }

    private void CalculateStep()
    {
        if (_fromCurrentValue)
        {
            from = (float)field.GetValue(_component);
        }
        else
        {
            field.SetValue(_component, from);
        }
        step = ((to - from) / _timeInterval) / 50.0f;
    }

    [ContextMenu("starttween")]
    public void StartTween()
    {
        CalculateStep();
        run = true;
    }


    public void FixedUpdate()
    {
        float currentValue = (float)field.GetValue(_component);
        if (!run)
        {
            return;
        }
        if (Mathf.Abs(currentValue - to) < Mathf.Abs(step))
        {
            run = false;
            TweenComplete?.Invoke();
            return;
        }
        if (run)
        {
            field.SetValue(_component, currentValue + step);
        }
        
    }

}
