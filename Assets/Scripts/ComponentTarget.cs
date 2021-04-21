using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentTarget : Damageable
{
    
    private MechHealth parent;
    
    void Start()
    {
        parent = GetComponentInParent<MechHealth>();
        if(parent == null)
        {
            Debug.LogError("COULDN'T FIND MECHHEALTH PARENT!");
        }
    }

    public override void TakeDamage(float damage)
    {
        Debug.Log($"Damage Taken from {gameObject.name} : {damage}");
        parent.TakeDamage(damage);
    }
}
