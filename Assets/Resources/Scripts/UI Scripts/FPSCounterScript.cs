using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCounterScript : MonoBehaviour
{
    private TMPro.TextMeshProUGUI FPSCounterText;

    //Start
    private void Start()
    {
        FPSCounterText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    //Update
    private void Update()
    {
        FPSCounterText.text = "FPS: " + Mathf.Round(1 / Time.smoothDeltaTime);
    }
}
