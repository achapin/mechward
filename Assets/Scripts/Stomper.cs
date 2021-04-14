using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Stomper : MonoBehaviour
{
    [SerializeField]
    float liftHeight;

    [SerializeField]
    float rotationSpeed;

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

    private Vector3 intialCenterofMass;

    private bool IsMoving => Mathf.Abs(pivotInput) > Mathf.Epsilon;

    private Transform lifted;
    private Transform liftedRestPoint;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody>();
        intialCenterofMass = myRigidbody.centerOfMass;
    }

    void FixedUpdate()
    {
        //myRigidbody.isKinematic = IsMoving;

        if(IsMoving)
        {
            bool pivotLeft = pivotInput < 0;
            var pivotPoint = pivotLeft ? LeftLeg : RightLeg;
            var reversed = reverseInput ? 1f : -1f;
            myTransform.RotateAround(pivotPoint.position, pivotPoint.up, rotationSpeed * Time.deltaTime * Mathf.Sign(pivotInput) * reversed);
            if(lifted == null)
            {
                lifted = pivotLeft ? RightLeg : LeftLeg;
                var liftPosition = pivotLeft ? RightLegLiftPoint : LeftLegLiftPoint;
                lifted.localPosition = liftPosition.localPosition;
                liftedRestPoint = pivotLeft ? RightLegRestPoint : LeftLegRestPoint;
                myRigidbody.centerOfMass = pivotPoint.localPosition;
            }
        } 
        else
        {
            if(lifted != null)
            {
                lifted.localPosition = liftedRestPoint.localPosition;
                lifted = null;
                myRigidbody.centerOfMass = intialCenterofMass;
            }
        }
    }

    public bool ShouldFollow ()
    {
        return !IsMoving;
    }
    
    public void SetPivot(InputAction.CallbackContext context)
    {
        pivotInput = context.ReadValue<float>();
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
