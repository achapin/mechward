using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    [SerializeField]
    private PlayerMechHealth mechHealth;

    void Start()
    {
        mechHealth.DisableMech();    
    }

    void OnDisable()
    {
        mechHealth.EnableMech();
    }
}
