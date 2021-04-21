using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHealth : Damageable
{
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

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
        }else{
            Debug.Log("Current Health : " + currentHealth);
        }
        //TODO: when health reaches 0, do something
    }

}
