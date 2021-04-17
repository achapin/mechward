using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireControl : MonoBehaviour
{

    private bool isFiring;

    public bool IsFiring => isFiring;

    void Update()
    {
        
    }

    public void SetIsFiring(InputAction.CallbackContext context)
    {
        isFiring = context.ReadValueAsButton();
    }
}
