using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject PausedCanvas;
    CanvasGroup Group;

    private void Awake()
    {
        Group = PausedCanvas.GetComponent<CanvasGroup>();
        Group.alpha = 0;
    }

    public void Pause()
    {
        Debug.Log("Pause");

        //Set Normal UI Canvas invisible
        GetComponent<Canvas>().enabled = false;

        //Activate Paused UI Canvas
        StartCoroutine(PauseAnimation());

        //Muffle the music and mute SFX
        MusicPlayerScript.MusicScript.FadeMusic(true);

        //Stop Time Scale
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;
    }

    IEnumerator PauseAnimation()
    {
        //Show the pause screen with fade-in animation
        PausedCanvas.GetComponent<Canvas>().enabled = true;

        float count = 0;
        while (count <= 0.75)
        {
            //Change opacity of canvas
            Group.alpha = count / 0.75f;

            //Increment count variable based on framerate
            if (Application.targetFrameRate == 30)
            {
                count += 0.1f;
            }
            else
            {
                count += 0.05f;
            }

            yield return new WaitForSecondsRealtime(0.001f);
        }

        Group.alpha = 1;

        yield return null;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieScript>().Dead)
        {
            Pause();
        }
    }
}
