using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VelocityCondition : MonoBehaviour
{
    [Header("Requred Velocity")]
    public float Value;
    public float Interval = 0.0f;

    [Space]
    public InteractionEvent VelocityGreaterOrEqual;
    public InteractionEvent VelocityLess;

    public InteractionEvent WithinInterval;
    public InteractionEvent OutOfInterval;


    public void CheckVelocity(GameObject obj)
    {
        var body = obj.GetComponent<Rigidbody2D>();

        Debug.Log(body.velocity.magnitude);
        Debug.Log(body.velocity);
        Debug.Log(obj);

        if (body.velocity.magnitude >= Value)
        {
            VelocityGreaterOrEqual?.Invoke(obj);
        }
        else
        {
            VelocityLess?.Invoke(obj);
        }

        // Debug.Log(body.velocity.magnitude);
        if (body.velocity.magnitude >= Value - Interval && body.velocity.magnitude <= Value + Interval)
        {
            WithinInterval?.Invoke(obj);
        }
        else
        {
            OutOfInterval?.Invoke(obj);
        }
    }


}
