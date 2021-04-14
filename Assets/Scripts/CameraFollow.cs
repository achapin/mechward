using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField]
    private Transform toFollow;

    [SerializeField]
    private Vector3 targetOffset;

    private Stomper myStomper;

    void Start()
    {
        myStomper = toFollow.gameObject.GetComponentInChildren<Stomper>();
    }

    void Update()
    {
        if(myStomper.ShouldFollow())
        {
            transform.position = Vector3.Lerp(transform.position, toFollow.TransformPoint(targetOffset), .1f);
        }
        transform.LookAt(toFollow, Vector3.up);
    }

    void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position, toFollow.TransformPoint(targetOffset), Color.red);
        Debug.DrawLine(toFollow.position, toFollow.TransformPoint(targetOffset), Color.green);
    }
}
