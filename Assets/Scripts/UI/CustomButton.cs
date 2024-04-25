using UnityEngine.UI;
using UnityEngine;


public class CustomButton : Button
{
    [SerializeField] GameObject _normal;
    [SerializeField] GameObject _pressed;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);

        _normal.SetActive(state != SelectionState.Pressed);
        _pressed.SetActive(state == SelectionState.Pressed);
    }
}
