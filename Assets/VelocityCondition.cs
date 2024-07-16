using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VelocityCondition : MonoBehaviour
{
    [Header("Requred Velocity")]
    public float Value;

    [Space]
    public InteractionEvent VelocityGreaterOrEqual;
    public InteractionEvent VelocityLess;


    public void CheckVelocity(GameObject obj)
    {
        var body = obj.GetComponent<Rigidbody2D>();
        if (body.velocity.magnitude >= Value)
        {
            VelocityGreaterOrEqual?.Invoke(obj);
        }
        else
        {
            VelocityLess?.Invoke(obj);
        }
    }


}
