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

    [SerializeField]
    private float liftDot;

    [SerializeField]
    private float closestDistance;

    private Stomper stomper;
    private Elevator elevator;
    private FireControl fireControl;

    private bool pivotRight;
    private bool shouldPivotRight;
    private Vector3 rotationTarget;
    private float oppositeLength = 10f;

    private float countdown;

    private float lastAngle;

    private bool canSeeTarget;

    void Start()
    {
        stomper = GetComponent<Stomper>();    
        elevator = GetComponentInChildren<Elevator>();
        fireControl = GetComponent<FireControl>();
    }

    void Update()
    {
        CheckCanSeeTarget();
        UpdateMovement();
        UpdateTargeting();
    }

    void CheckCanSeeTarget()
    {
        Color connectColor = Color.red;
        if(Physics.Raycast(transform.position, target.transform.position - transform.position, out RaycastHit hitinfo))
        {
            var checkObject = hitinfo.collider?.gameObject;
            while(checkObject != target.gameObject && checkObject != null)
            {
                checkObject = checkObject.transform.parent?.gameObject;
            }
            if(checkObject != target.gameObject)
            {
                stomper.SetInput(0f);
                Debug.DrawLine(target.transform.position, transform.position, connectColor);
                canSeeTarget = false;
                return;
            }
            connectColor = Color.green;
        }
        Debug.DrawLine(target.transform.position, transform.position, connectColor);
        canSeeTarget = true;
    }

    private void UpdateMovement()
    {
        if(Vector3.Dot(transform.up, Vector3.up) < liftDot)
        {
            stomper.SetInput(0f);
            return;
        }

        if(countdown > 0f){
            countdown -= Time.deltaTime;
            stomper.SetInput(0f);
            return;
        }

        if(!canSeeTarget)
        {
            stomper.SetInput(0f);
            return;
        }

        if(Vector3.Distance(target.transform.position, transform.position) <= closestDistance)
        {
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
        Debug.DrawLine(transform.position, rotationTarget, Color.yellow);       
    }

    private void UpdateTargeting()
    {
        if(!canSeeTarget)
        {
            fireControl.SetIsFiring(false);
            return;
        }

        Vector3 targetPoint = elevator.transform.InverseTransformPoint(target.transform.position);

        if(Mathf.Abs(targetPoint.y) < 2f)
        {
            fireControl.SetIsFiring(true);
        }
        else
        {
            fireControl.SetIsFiring(false);
            elevator.SetElevate(-Mathf.Sign(targetPoint.y));
        }
    }
}
