using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButtonScript : MonoBehaviour
{
    public Canvas TitleScreenCanvas;
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
