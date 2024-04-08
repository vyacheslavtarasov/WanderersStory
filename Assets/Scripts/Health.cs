using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChangeHealthEvent : UnityEvent<float, float, float> // was, became, all
{

}

public class Health : MonoBehaviour
{
    // Start is called before the first frame update
    public float overallHealth = 6.0f;
    public float currentHealth;
    public UnityEvent OnDead;
    public ChangeHealthEvent OnChangeHealth;

    public UnityEvent OnHeal;
    public UnityEvent OnDamage;
    

    private void Start()
    {
        currentHealth = overallHealth;
    }

    public void ChangeHealth(float amount, GameObject inflicter = null)
    {
        var wasHealth = currentHealth;
        currentHealth += amount;
        OnChangeHealth?.Invoke(wasHealth, currentHealth, overallHealth);

        if (currentHealth <= 0.0f)
        {
            OnDead?.Invoke();
            return;
        }
        
        if (amount < 0)
        {
            OnDamage?.Invoke();
            return;
        }
        OnHeal?.Invoke();

    }
}