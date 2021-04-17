using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnDamage : Damageable
{

    [SerializeField]
    private float health;

    private float currentHealth;

    void Start()
    {
        currentHealth = health;
    }

    public override void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
