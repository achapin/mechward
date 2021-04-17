using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LegCollider : MonoBehaviour
{
    [SerializeField]
    private Transform footTransform;

    private CapsuleCollider myCollider;

    void Start()
    {
        myCollider = GetComponent<CapsuleCollider>();
    }


    void FixedUpdate()
    {
        var distance = Vector3.Distance(footTransform.position, transform.position);
        myCollider.height = distance;
        myCollider.center = Vector3.down * distance * .5f;
    }
}
