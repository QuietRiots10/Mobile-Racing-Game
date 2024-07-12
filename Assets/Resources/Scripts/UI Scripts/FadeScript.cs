using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public static FadeScript Fade = null;
    Image FadeImage;
    public bool Faded;
    
    private void Awake()
    {
        if (Fade == null)
        {
            Fade = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        FadeImage = transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public IEnumerator FadeToBlack()
    {
        //Debug.Log("Fade To Black");
        Faded = true;
        FadeImage.color = new Color(0, 0, 0, 0);
        FadeImage.enabled = true;

        float count = 0;
        while (count <= 0.5f)
        {
            count = count + Time.deltaTime;
            FadeImage.color = new Color(0, 0, 0, 2 * count);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        FadeImage.color = new Color(0, 0, 0, 1);

        yield return null;
    }

    public IEnumerator FadeFromBlack() 
    {
       //Debug.Log("Fade From Black");
        FadeImage.color = new Color(0, 0, 0, 1);
        FadeImage.enabled = true;

        float count = 0.5f;
        while (count >= 0)
        {
            count = count - Time.deltaTime;
            FadeImage.color = new Color(0, 0, 0, 2 * count);
            yield return new WaitForSecondsRealtime(0.001f);
        }
        Faded = false;
        //FadeImage.color = FadeImage.color = new Color(0, 0, 0, 0);
        FadeImage.enabled = false;

        yield return null;
    }
}
