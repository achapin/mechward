using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDisplay : MonoBehaviour
{
    
    [SerializeField]
    private Renderer[] renderersToUpdate;

    [SerializeField]
    private LaserWeapon batteryTarget;

    [SerializeField]
    private Color fullColor;

    [SerializeField]
    private Color offColor;

    void Update()
    {
        for(int index = 0; index < renderersToUpdate.Length; index++)
        {
            float minForThisPoint = (float) index / renderersToUpdate.Length;
            float maxForThisPoint = (float) (index + 1) / renderersToUpdate.Length;

            if(batteryTarget.CurrentBatteryLevel() > maxForThisPoint)
            {
                renderersToUpdate[index].materials[0].color = fullColor;
                continue;
            }

            if(batteryTarget.CurrentBatteryLevel() < minForThisPoint)
            {
                renderersToUpdate[index].materials[0].color = offColor;
                continue;
            }

            var displayPoint = Mathf.InverseLerp(minForThisPoint, maxForThisPoint, batteryTarget.CurrentBatteryLevel());

            renderersToUpdate[index].materials[0].color = Color.Lerp(offColor, fullColor, displayPoint);
        }
    }
}
