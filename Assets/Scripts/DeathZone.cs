using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    private LevelManager manager;

    void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Damageable>(out Damageable damageable))
        {
            if(damageable is ComponentTarget)
            {
                var parent = (damageable as ComponentTarget).Mech;
                if(parent.TryGetComponent<PlayerMechHealth>(out PlayerMechHealth _))
                {
                    manager.GameOver();
                }
            }
        }
    }
}
