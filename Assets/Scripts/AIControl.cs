using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stomper))]
public class AIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private Stomper stomper;

    private bool pivotRight;
    private Vector3 rotationTarget;
    private float oppositeLength = 10f;

    void Start()
    {
        stomper = GetComponent<Stomper>();    
    }

    // Update is called once per frame
    void Update()
    {
        var pingPongVal = 1f - Mathf.PingPong(Time.time, 2f);
        if(Mathf.Abs(pingPongVal) < .3f)
        {
            pingPongVal = 0f;
        }else{
            pingPongVal = Mathf.Sign(pingPongVal);
        }
        stomper.SetInput(pingPongVal);

        /*
        Vector3 flatTargetPos = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Vector3 flatMyPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetLine = flatTargetPos - flatMyPos;

        var angle = Mathf.Atan(oppositeLength / targetLine.magnitude);

        rotationTarget = new Vector3(flatTargetPos.x + Mathf.Cos(angle) * oppositeLength, 0f, flatTargetPos.z + Mathf.Sin(angle) * targetLine.magnitude);
        Debug.DrawLine(flatMyPos, rotationTarget, Color.blue);
        Debug.DrawLine(flatTargetPos, rotationTarget, Color.red);
        Debug.DrawLine(flatTargetPos, flatMyPos, Color.green);
        */
    }
}
