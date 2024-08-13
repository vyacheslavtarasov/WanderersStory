using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnabledCondition : MonoBehaviour
{
    [Header("Required Enabled Collider")]
    public Collider2D Collider;

    [Space]
    public InteractionEventWithSender ColliderEnabled;
    public InteractionEventWithSender ColliderDisabled;


    public void CheckCollider(GameObject initiator = null, GameObject source = null)
    {
        var collider = initiator.GetComponent<Collider2D>();
        if (collider != null)
        {
            if (collider.enabled)
            {
                ColliderEnabled?.Invoke(initiator, source);
                return;
            }
        }

        ColliderEnabled?.Invoke(initiator, source);
    }
}
