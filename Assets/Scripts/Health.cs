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

    private bool dead = false;

    public bool Invictible { get; set; }
    

    private void Start()
    {
        currentHealth = overallHealth;
    }

    private void OnEnable()
    {
        if (currentHealth > 0)
        {
            dead = false;
        }
    }

    public void ChangeHealth(float amount, GameObject inflicter = null)
    {
        if (dead) { return; }
        var wasHealth = currentHealth;

        if (Invictible && amount < 0)
        {
            return;
        }

        currentHealth += amount;


        if (currentHealth > 50)
        {
            currentHealth = 50;
        }

        

        OnChangeHealth?.Invoke(wasHealth, currentHealth, overallHealth);

        if (currentHealth <= 0.0f && !dead)
        {
            OnDead?.Invoke();

            dead = true;
            return;
        }
        
        if (amount < 0 && !dead)
        {
            OnDamage?.Invoke();
            return;
        }
        OnHeal?.Invoke();

    }
}