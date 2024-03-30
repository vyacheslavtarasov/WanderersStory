using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class InteractionEventWithSender : UnityEvent<GameObject, GameObject>
{

}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Interactive : MonoBehaviour
{
    public InteractionEventWithSender InteractEvent;
    public void Interact(GameObject initiator)
    {
        InteractEvent?.Invoke(initiator, this.gameObject);
    }
}
