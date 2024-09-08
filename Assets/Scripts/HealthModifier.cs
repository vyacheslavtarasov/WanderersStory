using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    public float Amount = 1.0f;
    public GameObject TargetObject { get; set; }
    public void Modify(GameObject targetObject)
    {
        if (targetObject == null)
        {
            targetObject = TargetObject;
        }
        if (targetObject == null) return;

        var healthComponent = targetObject?.GetComponent<Health>();

        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(Amount, this.gameObject);
        }
    }

    public void Modify()
    {
        if (TargetObject == null) return;

        var healthComponent = TargetObject?.GetComponent<Health>();

        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(Amount, this.gameObject);
        }
    }
}