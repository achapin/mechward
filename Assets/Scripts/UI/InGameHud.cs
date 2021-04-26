using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameHud : MonoBehaviour
{
    [SerializeField]
    private LevelManager levelManager;

    [SerializeField]
    private TextMeshProUGUI text;

    void Update()
    {
        var remainingSeconds = levelManager.RemainingSeconds;
        var remainingMinutes = remainingSeconds / 60;
        remainingSeconds -= remainingMinutes * 60;
        text.text = $"{remainingMinutes}:{remainingSeconds}";
    }
}
