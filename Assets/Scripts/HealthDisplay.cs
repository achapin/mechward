using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    private MechHealth healthToTrack;

    void Update()
    {
        transform.localScale = new Vector3(Mathf.Clamp01(healthToTrack.HealthPct), 1f, 1f);
    }
}
