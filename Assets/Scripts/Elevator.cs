using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Elevator : MonoBehaviour
{
    private Transform myTransform;

    [SerializeField]
    private Vector2 minMaxAngle;

    [SerializeField]
    private float elevatorSpeed;

    private float currentRotation;
    private float lastInput;

    void Start()
    {
        myTransform = GetComponent<Transform>();
        currentRotation = 0f;
    }
    

    void Update()
    {
        if(Mathf.Abs(lastInput) <= Mathf.Epsilon)
        {
            return;
        }

        var deltaAngle = elevatorSpeed * lastInput * Time.deltaTime;

        if(deltaAngle + currentRotation < minMaxAngle.x)
        {
            deltaAngle = minMaxAngle.x - currentRotation;
        }
        
        if(deltaAngle + currentRotation > minMaxAngle.y)
        {
            deltaAngle = minMaxAngle.y - currentRotation;
        }

        myTransform.Rotate(Vector3.right * deltaAngle);
        currentRotation += deltaAngle;
    }

    public void SetElevate(InputAction.CallbackContext context)
    {
        lastInput = context.ReadValue<float>();
    }
}
