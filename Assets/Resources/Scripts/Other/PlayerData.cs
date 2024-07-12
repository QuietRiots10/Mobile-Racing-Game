using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    //Start
    void Start()
    {
        //Set high score to 0 if the player does not have one
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }

        //Set tutorial flag to 0 if the player does not have one
        if (!PlayerPrefs.HasKey("DoneTutorial"))
        {
            PlayerPrefs.SetInt("DoneTutorial", 0);
        }

        //Set the music and SFX volume to 100 if the player has not set it yet
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            //Volumes are stored betwene 0 and 100
            PlayerPrefs.SetFloat("MusicVolume", 100);
            PlayerPrefs.SetFloat("SFXVolume", 100);
        }
    }
}
