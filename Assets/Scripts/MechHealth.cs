using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechHealth : Damageable
{
    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private MonoBehaviour[] disableWhenShutdown;

    [SerializeField]
    private float disableTime;

    private float currentHealth;
    private float disableCount;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if(disableCount > 0f)
        {
            disableCount -= Time.deltaTime;
            if(disableCount <= 0f)
            {
                foreach(var component in disableWhenShutdown)
                {
                    component.enabled = true;
                }
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        if(disableTime > 0f) 
        {
            return;
        }
        currentHealth -= damage;
        if(currentHealth <= 0f)
        {
            Debug.Log("BOOM!");
            disableCount = disableTime;
            foreach(var component in disableWhenShutdown)
            {
                component.enabled = false;
            }
        }else{
            Debug.Log("Current Health : " + currentHealth);
        }
        //TODO: when health reaches 0, do something
    }

}
