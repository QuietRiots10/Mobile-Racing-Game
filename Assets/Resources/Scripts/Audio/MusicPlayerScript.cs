using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    //Objects
    public static MusicPlayerScript MusicScript = null;
    PlayerDieScript DeathScript;
    AudioSource MusicPlayer;

    //Variables
    public bool TitleScreen;
    
    private AudioClip RandomTrack(bool title)
    {
        if (title == true)
        {
            return Resources.Load("Audio/TitleMusic/1") as AudioClip;
        }
        else
        {
            int rand = Mathf.RoundToInt(Random.Range(1, 5));
            return Resources.Load("Audio/Music/" + rand) as AudioClip;
        }
        
    }

    private void PlayTrack (AudioClip clip)
    {
        MusicPlayer.clip = clip;
        MusicPlayer.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;
        MusicPlayer.Play();
    }

    public void FadeMusic (bool fade)
    {
        if (fade)
        {
            GetComponent<AudioLowPassFilter>().enabled = true;
        }
        else
        {
            GetComponent<AudioLowPassFilter>().enabled = false;
        }
    }

    public void StopMusic()
    {
        MusicPlayer.Stop();
    }

    //Awake
    private void Awake()
    {
        //Make a public instance of the music player
        if (MusicScript == null)
        {
            MusicScript = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        //Define the MusicPlayer
        MusicPlayer = GetComponent<AudioSource>();

        //Play a track
        if (TitleScreen)
        {
            //Plays a random track from the title screen music
            PlayTrack(RandomTrack(true));
        }
        else
        {
            //Define DeathScript
            DeathScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieScript>();

            //Plays a random track from the game music
            PlayTrack(RandomTrack(false));
        }
    }

    private void LateUpdate()
    {
        //Sets the volume of the Music PLayer
        MusicPlayer.volume = PlayerPrefs.GetFloat("MusicVolume") / 100;

        //Stops music when player dies
        if (!TitleScreen)
        {
            if (DeathScript.Dead)
            {
                StopMusic();
            }
        } 
    }
}