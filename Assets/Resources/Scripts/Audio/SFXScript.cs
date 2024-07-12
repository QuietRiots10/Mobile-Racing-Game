using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFXScript : MonoBehaviour
{
    //Objects

    public AudioMixer Master;
    public bool TitleScreen;
    public PlayerTurnScript TurnScript;
    public Canvas DeathCanvas;
    public Canvas PauseCanvas;
    public AudioSource PersistientSoundPlayer;
    public AudioClip Driving;
    public AudioClip Drifting;
    float DefaultPersistientVolume;
    string SelectedClip;

    private void Start()
    {
        if (!TitleScreen) { DefaultPersistientVolume = PersistientSoundPlayer.volume; }
    }

    //Update
    private void LateUpdate()
    {
        //Update the volume of the SFX channels to reflect player settings
        Master.SetFloat("Volume", ((PlayerPrefs.GetFloat("SFXVolume") / 100) * 80) - 80);

        
        if (!TitleScreen)
        {
            //Mute driving sounds when game is paused
            if (DeathCanvas.enabled || PauseCanvas.enabled)
            {
                PersistientSoundPlayer.volume = 0;
            }
            else
            {
                PersistientSoundPlayer.volume = DefaultPersistientVolume;
            }

            //Change sound to drifting if turning sharply
            if (Mathf.Abs(TurnScript.TargetRot) > 0.75f && SelectedClip != "Drifting")
            {
                PersistientSoundPlayer.Stop();

                SelectedClip = "Drifting";
                PersistientSoundPlayer.clip = Drifting;

                PersistientSoundPlayer.Play();
            }
            else if (Mathf.Abs(TurnScript.TargetRot) <= 0.75f && SelectedClip != "Driving")
            {
                PersistientSoundPlayer.Stop();

                SelectedClip = "Driving";
                PersistientSoundPlayer.clip = Driving;

                PersistientSoundPlayer.Play();
            }
        } 

        
    }
}
