using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AngleCondition : MonoBehaviour
{
    public float From;
    public float To;

    [Space]
    public InteractionEvent Ok;
    public InteractionEvent NotOk;


    public void CheckDirection(GameObject obj)
    {

        var body = obj.GetComponent<Rigidbody2D>();
        float velocityAngle = Vector2.SignedAngle(Vector2.right, body.velocity);
        // Debug.Log(velocityAngle);
        if (velocityAngle > From && velocityAngle < To)
        {
            Ok?.Invoke(obj);
        }
        else
        {
            NotOk?.Invoke(obj);
        }
    }
}