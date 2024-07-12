using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject UICanvas;
    public Slider SFXSlider;
    public Slider MusicSlider;

    private void Awake()
    {
        //Fade in
        try
        {
            if (FadeScript.Fade.Faded)
            {
                StartCoroutine(FadeFromBlack());
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        //Set sliders to their desired values from PlayerPrefs
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    private IEnumerator FadeFromBlack()
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        FadeScript.Fade.StartCoroutine(FadeScript.Fade.FadeFromBlack());
        yield return new WaitForSecondsRealtime(0.5f);

        StartCoroutine(Unpause());
    }

    private IEnumerator FadeToBlack()
    {
        //Restore Time Scale
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        FadeScript.Fade.StartCoroutine(FadeScript.Fade.FadeToBlack());
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadScene("TitleScreen");
    }

    public void UnpauseButton()
    {
        StartCoroutine(Unpause());
    }

    private IEnumerator Unpause()
    {
        //Hide the pause screen with fade-out animation
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

        //Unmuffle the music and SFX
        MusicPlayerScript.MusicScript.FadeMusic(false);

        //Restore Time Scale
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        //Activate normal UI Canvas
        UICanvas.GetComponent<Canvas>().enabled = true;
    }

    public void RestartButton()
    {
        StartCoroutine(Unpause());
        StartCoroutine(Restart());
    }

    private IEnumerator Restart()
    {
        PlayerPrefs.Save();
        FadeScript.Fade.StartCoroutine(FadeScript.Fade.FadeToBlack());
        UICanvas.GetComponent<Canvas>().enabled = false;

        //Restore Time Scale
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        yield return new WaitForSecondsRealtime(0.5f);
        UICanvas.GetComponent<Canvas>().enabled = false;

        SceneManager.LoadSceneAsync("MainScene");
    }

    public void SFXSliderChange()
    {
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }

    public void MusicSliderChange()
    {
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void MainMenu()
    {
        StartCoroutine(FadeToBlack());
    }
}
