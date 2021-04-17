using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWeapon : Weapon
{
    [SerializeField]
    private float maxBattery;

    [SerializeField]
    private float batteryDrainWhileFiring;

    [SerializeField]
    private float batteryChargeWhileResting;

    [SerializeField]
    private float damagePerSecond;

    [SerializeField]
    private LineRenderer laserLine;

    private float currentBattery;

    void Start()
    {
        currentBattery = maxBattery;
        laserLine.positionCount = 2;
    }

    void Update()
    {
        laserLine.SetPosition(0, firePoint.position);
        if(myFireControlSystem.IsFiring)
        {
            currentBattery = Mathf.Clamp(currentBattery - batteryDrainWhileFiring * Time.deltaTime, 0f, maxBattery);
            if(currentBattery > 0f)
            {
                laserLine.enabled = true;
                Debug.DrawLine(firePoint.position, firePoint.position + firePoint.forward * 1000f, Color.red);
                if(Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit hit, 1000f))
                {
                    laserLine.SetPosition(1, hit.point);

                    if(hit.collider.gameObject.TryGetComponent(out Damageable damageable))
                    {
                        damageable.TakeDamage(damagePerSecond * Time.deltaTime);
                    }
                }
                else
                {
                    laserLine.SetPosition(1, firePoint.position + firePoint.forward * 1000f);
                }
            }
            else 
            {
                laserLine.enabled = false;
            }
        }
        else
        {
            laserLine.enabled = false;
            currentBattery = Mathf.Clamp(currentBattery + batteryChargeWhileResting * Time.deltaTime, 0f, maxBattery);
        }
    }
}
