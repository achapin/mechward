using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Stomper))]
public class AIControl : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Transform rabbit;

    private Stomper stomper;

    private bool pivotRight;
    private bool shouldPivotRight;
    private Vector3 rotationTarget;
    private float oppositeLength = 10f;

    private float countdown;

    private float lastAngle;

    void Start()
    {
        stomper = GetComponent<Stomper>();    
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var pingPongVal = 1f - Mathf.PingPong(Time.time, 2f);
        if(Mathf.Abs(pingPongVal) < .3f)
        {
            pingPongVal = 0f;
        }else{
            pingPongVal = Mathf.Sign(pingPongVal);
        }
        stomper.SetInput(pingPongVal);
        */
        
        /*
        Vector3 flatTargetPos = new Vector3(target.transform.position.x, 0f, target.transform.position.z);
        Vector3 flatMyPos = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 targetLine = flatTargetPos - flatMyPos;

        var targetDist = targetLine.magnitude;

        var angle = Mathf.Atan2(oppositeLength, targetDist);

        var hypLength = oppositeLength * Mathf.Cos(angle);
        //rotationTarget = new Vector3(Mathf.Cos(angle) * oppositeLength, 0f, Mathf.Sin(angle) * targetLine.magnitude);
        */

        if(countdown > 0f){
            countdown -= Time.deltaTime;
            stomper.SetInput(0f);
            return;
        }

        rabbit.LookAt(target.transform, Vector3.up);
        var rabbitPos = rabbit.InverseTransformPoint(target.transform.position);
        var rabbitPos1 = rabbit.TransformPoint(rabbitPos + (Vector3.right * oppositeLength));
        var rabbitPos2 = rabbit.TransformPoint(rabbitPos + (Vector3.left * oppositeLength));
        
        var angle1 = Vector3.Angle(transform.forward, rabbitPos1 - transform.position);
        var angle2 = Vector3.Angle(transform.forward, rabbitPos2 - transform.position);

        var angleDiff = lastAngle - (pivotRight ? angle1 : angle2);
        lastAngle = pivotRight ? angle1 : angle2;

        if(angleDiff < 0f)
        {
            shouldPivotRight = angle1 > angle2;
            if(shouldPivotRight != pivotRight)
            {
                pivotRight = shouldPivotRight;
                countdown = .25f;
            }
        }
        rotationTarget = pivotRight ? rabbitPos1 : rabbitPos2;
        stomper.SetInput(pivotRight ? 1f : -1f);


        Debug.DrawLine(target.transform.position, rotationTarget, Color.blue);
        Debug.DrawLine(transform.position, rotationTarget, Color.red);
        Debug.DrawLine(target.transform.position, transform.position, Color.green);
        
    }
}
