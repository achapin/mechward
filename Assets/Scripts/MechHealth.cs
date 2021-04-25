using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHealth : Damageable
{
    [SerializeField]
    protected float maxHealth;

    protected float currentHealth;

    public float HealthPct => currentHealth / maxHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0f)
        {
            Debug.Log("BOOM!");
            OnDestroy();
        }else{
            Debug.Log("Current Health : " + currentHealth);
        }
    }

    protected virtual void OnDestroy()
    {
    }

}
