using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModifier : MonoBehaviour
{
    public float Amount = 1.0f;
    public void Modify(GameObject targetObject)
    {
        var healthComponent = targetObject?.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(Amount, this.gameObject);
        }
    }
}