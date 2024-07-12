using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenHighScore : MonoBehaviour
{
    //Awake
    void LateUpdate()
    {
        //Set the text of the high score label
        GetComponent<TMPro.TextMeshProUGUI>().text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
    }
}
