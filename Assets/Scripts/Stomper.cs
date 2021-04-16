using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stomper : MonoBehaviour
{
    [SerializeField]
    float rotationForce;

    [SerializeField]
    float liftForce;

    [SerializeField]
    float liftDot;

    [SerializeField]
    private Transform defaultCenterOfMass;

    [SerializeField]
    private Transform LeftLeg;

    [SerializeField]
    private Transform LeftLegLiftPoint;

    [SerializeField]
    private Transform LeftLegRestPoint;

    [SerializeField]
    private Transform RightLeg;

    [SerializeField]
    private Transform RightLegLiftPoint;

    [SerializeField]
    private Transform RightLegRestPoint;

    private Transform myTransform;
    private Rigidbody myRigidbody;

    private float pivotInput;
    private bool reverseInput = true;

    private bool IsMoving => Mathf.Abs(pivotInput) > Mathf.Epsilon;

    private float lastInput;

    private Transform lifted;
    private Transform liftedRestPoint;
    private Transform liftedLiftPoint;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.centerOfMass = defaultCenterOfMass.localPosition;
    }

    void FixedUpdate()
    {

        if(Vector3.Dot(myTransform.up, Vector3.up) < liftDot)
        {
            myRigidbody.AddForceAtPosition(Vector3.up * liftForce, myTransform.position);
        }

        if(IsMoving)
        {
            bool pivotLeft = pivotInput < 0;
            var pivotPoint = pivotLeft ? LeftLeg : RightLeg;
            var reversed = reverseInput ? 1f : -1f;
            var pushPoint = pivotLeft ? RightLegRestPoint : LeftLegRestPoint;
            myRigidbody.AddForceAtPosition(pushPoint.forward * rotationForce * reversed, pushPoint.position);
            if(lifted == null)
            {
                lifted = pivotLeft ? RightLeg : LeftLeg;
                var liftPosition = pivotLeft ? RightLegLiftPoint : LeftLegLiftPoint;
                liftedRestPoint = pivotLeft ? RightLegRestPoint : LeftLegRestPoint;
                liftedLiftPoint = pivotLeft ? RightLegLiftPoint : LeftLegLiftPoint;
                myRigidbody.centerOfMass = pivotPoint.localPosition;
            }
            lifted.localPosition = Vector3.Lerp(lifted.localPosition, liftedLiftPoint.localPosition, .3f);
        } 
        else
        {
            if(lifted != null)
            {
                lifted.localPosition = Vector3.Lerp(lifted.localPosition, liftedRestPoint.localPosition, .3f);
                if(Vector3.Distance(lifted.localPosition, liftedRestPoint.localPosition) < .05f)
                {
                    lifted.localPosition = liftedRestPoint.localPosition;
                    lifted = null;
                }
                myRigidbody.centerOfMass = defaultCenterOfMass.localPosition;
            } else {
                if(Mathf.Abs(lastInput) > Mathf.Epsilon){
                    pivotInput = lastInput;
                }
            }
        }
    }

    public bool ShouldFollow ()
    {
        return !IsMoving;
    }
    
    public void SetPivot(InputAction.CallbackContext context)
    {
        lastInput = context.ReadValue<float>();
        if(IsMoving && Mathf.Abs(lastInput) <= Mathf.Epsilon)
        {
            pivotInput = 0f;
        }
    }

    public void SetReverse(InputAction.CallbackContext context)
    {
        reverseInput = !context.ReadValueAsButton();
    }

    private void OnDrawGizmos()
    {
        if(myTransform == null)
        {
            myTransform = GetComponent<Transform>();
        }
        if(myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody>();
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(myTransform.TransformPoint(myRigidbody.centerOfMass), .25f);
    }
}
