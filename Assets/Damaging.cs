using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damaging : MonoBehaviour
{
    public float damageAmount = 1.0f;
    public void DoDamage(GameObject targetObject)
    {
        var healthComponent = targetObject?.GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.ChangeHealth(damageAmount, this.gameObject);
        }
    }
}