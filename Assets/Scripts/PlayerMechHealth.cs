using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMechHealth : MechHealth
{
    [SerializeField]
    private MonoBehaviour[] disableWhenShutdown;

    [SerializeField]
    private float disableTime;

    private float disableCount;

    void Update()
    {
        if(disableCount > 0f && currentHealth <= 0f)
        {
            disableCount -= Time.deltaTime;
            if(disableCount <= 0f)
            {
                EnableMech();
                currentHealth = maxHealth;
            }
        }
    }

    protected override void OnDestroy()
    {
        if(disableCount > 0f)
        {
            Debug.Log("SKIPPING ONDESTROY BECAUSE WE ARE DESTROYED");
            return;
        }
        disableCount = disableTime;
        DisableMech();
    }

    public void DisableMech()
    {
        foreach(var component in disableWhenShutdown)
        {
            Debug.Log($"{component.name} disabked");
            component.enabled = false;
        }
    }

    public void EnableMech()
    {
        foreach(var component in disableWhenShutdown)
        {
            component.enabled = true;
        }
    }
}
