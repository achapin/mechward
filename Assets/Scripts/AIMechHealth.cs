using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMechHealth : MechHealth
{
    [SerializeField]
    private Stomper stomper;

    [SerializeField]
    private FireControl fireControl;

    [SerializeField]
    private AIControl aIControl;

    [SerializeField]
    private float deathForce;

    private bool destroyed;

    protected override void OnDestroy()
    {
        if(destroyed)
        {
            return;
        }
        destroyed = true;

        Destroy(aIControl);
        stomper.enabled = false;
        fireControl.enabled = false;
        var rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.position;
        rb.AddForceAtPosition(transform.position, -transform.forward * deathForce);
    }
}
