using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuScript : MonoBehaviour
{
    public Canvas TitleScreenCanvas;
    public Canvas PauseMenuCanvas;
    public Slider SFXSlider;
    public Slider MusicSlider;

    //Awake
    void Awake()
    {
        //Set sliders to their desired values from PlayerPrefs
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void SFXSliderChange()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

    public void MusicSliderChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void RefreshSliders()
    {
        //Refreshes Sliders
        SFXSlider.value = 100;
        MusicSlider.value = 100;
    }

    public void OpenMenu()
    {
        TitleScreenCanvas.enabled = false;
        StartCoroutine(MenuFade(true));
    }

    public void CloseMenu()
    {
        TitleScreenCanvas.enabled = true;
        StartCoroutine(MenuFade(false));
    }

    private IEnumerator MenuFade(bool FadeIn)
    {
        if (FadeIn)
        {
            //Fade in
            GetComponent<Canvas>().enabled = true;
            CanvasGroup Group = GetComponent<CanvasGroup>();

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

        else
        {
            //Fade out 
            CanvasGroup Group = GetComponent<CanvasGroup>();

            float count = 0.75f;
            while (count > 0)
            {
                //Change opacity of canvas
                Group.alpha = count / 0.75f;

                //Increment count variable based on framerate
                if (Application.targetFrameRate == 30)
                {
                    count -= 0.1f;
                }
                else
                {
                    count -= 0.05f;
                }

                yield return new WaitForSecondsRealtime(0.001f);
            }

            Group.alpha = 0;
            GetComponent<Canvas>().enabled = false;
        }
    }
}
