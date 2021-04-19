using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Crusher : MonoBehaviour
{
    [SerializeField]
    private float damageOnCrush;

    private Vector3 lastPos;

    private BoxCollider myCollider;

    private bool doCrush;

    private List<Damageable> damagedAlready = new List<Damageable>();

    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
        lastPos = transform.localPosition;
    }

    void FixedUpdate()
    {
        Vector3 delta = transform.localPosition - lastPos;

        var dot = Vector3.Dot(delta, Vector3.down);
        doCrush = dot >= .5;
        
        lastPos = transform.localPosition;

        if(!doCrush)
        {
            damagedAlready.Clear();
            return;
        }

        var hits = Physics.OverlapBox(transform.position + myCollider.center - transform.up, myCollider.size / 2f, transform.rotation);

        foreach(var hit in hits)
        {
            if(hit.gameObject.TryGetComponent(out Damageable damageable))
            {
                if(!damagedAlready.Contains(damageable))
                {
                    damageable.TakeDamage(damageOnCrush);
                    damagedAlready.Add(damageable);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if(doCrush)
        {
            Gizmos.DrawWireCube(transform.position + myCollider.center - transform.up, myCollider.size / 2f);
        }
    }
}
