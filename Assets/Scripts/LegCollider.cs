using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class LegCollider : MonoBehaviour
{
    [SerializeField]
    private Transform footTransform;

    [SerializeField]
    private Transform displayTransform;

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
        displayTransform.localScale = new Vector3(1f, distance, 1f);
    }
}
