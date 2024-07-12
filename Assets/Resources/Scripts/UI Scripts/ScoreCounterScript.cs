using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounterScript : MonoBehaviour
{
    //Variables
    TMPro.TextMeshProUGUI ScoreText;
    PlayerDieScript DeathScript;
    public float Score;

    //Start
    private void Start()
    {
        ScoreText = GetComponent<TMPro.TextMeshProUGUI>();
        DeathScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieScript>();

        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
    }

    private void FixedUpdate()
    {
        if (!DeathScript.Dead)
        {
            Score += Time.fixedDeltaTime;
            ScoreText.text = "Score: " + Mathf.RoundToInt(Score).ToString();
            
            //Check if the current score is a high score
            if (Score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(Score));
            }
        }
    }
}
