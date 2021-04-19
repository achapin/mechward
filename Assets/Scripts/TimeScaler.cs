using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TimeScaler : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.digit1Key.isPressed)
        {
            Time.timeScale = 1f;
        }

        if(Keyboard.current.digit2Key.isPressed)
        {
            Time.timeScale = .5f;
        }

        if(Keyboard.current.digit3Key.isPressed)
        {
            Time.timeScale = .25f;
        }

        if(Keyboard.current.digit4Key.isPressed)
        {
            Time.timeScale = .1f;
        }
    }
}
