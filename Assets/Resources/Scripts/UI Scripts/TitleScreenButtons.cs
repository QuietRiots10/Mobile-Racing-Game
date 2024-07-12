using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenButtons : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;

        Application.targetFrameRate = Screen.currentResolution.refreshRate;

        StartCoroutine(FadeFromBlack());
    }

    public void StartButton()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeFromBlack()
    {
        FadeScript.Fade.StartCoroutine(FadeScript.Fade.FadeFromBlack());
        yield return new WaitForSecondsRealtime(0.5f);
    }

    private IEnumerator FadeToBlack()
    {
        FadeScript.Fade.StartCoroutine(FadeScript.Fade.FadeToBlack());
        yield return new WaitForSecondsRealtime(0.5f);

        SceneManager.LoadSceneAsync("MainScene");
    }
}
