using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;


public class TwoStateController : MonoBehaviour
{
    [SerializeField] private bool _state;

    public InteractionEventWithSender onOn;
    public InteractionEventWithSender onOff;

    public void Switch(GameObject initiator = null, GameObject source = null)
    {
        if (_state)
        {
            onOff?.Invoke(initiator, source);
            _state = false;
        }
        else
        {
            onOn?.Invoke(initiator, source);
            _state = true;
        }
    }
}
