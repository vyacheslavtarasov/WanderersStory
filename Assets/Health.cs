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
    [SerializeField] private float _damageForceToInflict = 200.0f;

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
        // let it be here for now
        if (amount < 0)
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.GetComponent<Rigidbody2D>().AddForce(Vector2.up * _damageForceToInflict, ForceMode2D.Force);
        }
        
    }
}