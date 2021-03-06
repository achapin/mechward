using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireControl : MonoBehaviour
{

    private bool isFiring;

    public bool IsFiring => enabled && isFiring;

    void OnDisable()
    {
        SetIsFiring(false);
    }

    public void SetIsFiring(InputAction.CallbackContext context)
    {
        SetIsFiring(context.ReadValueAsButton());
    }
    
    public void SetIsFiring(bool firing)
    {
        isFiring = firing;
    }
}
