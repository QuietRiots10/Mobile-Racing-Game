using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialAnimation : MonoBehaviour
{
    //Variables
    Slider RightSlider;
    Slider LeftSlider;

    private void Start()
    {
        RightSlider = GameObject.Find("TutorialRight").transform.GetChild(1).GetComponent<Slider>();
        LeftSlider = GameObject.Find("TutorialLeft").transform.GetChild(1).GetComponent<Slider>();

        RightSlider.gameObject.SetActive(false);
        LeftSlider.gameObject.SetActive(false);
    }

    public IEnumerator AnimationStart(string Direction)
    {
        GameObject.Find("Tutorial" + Direction).transform.GetChild(0).gameObject.SetActive(true);
        GameObject.Find("Tutorial" + Direction).transform.GetChild(1).gameObject.SetActive(true);

        yield return null;
    }

    public IEnumerator AnimationEnd()
    {
        GameObject.Find("TutorialRight").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("TutorialRight").transform.GetChild(1).gameObject.SetActive(false);

        GameObject.Find("TutorialLeft").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("TutorialLeft").transform.GetChild(1).gameObject.SetActive(false);

        yield return null;
    }

    private void FixedUpdate()
    {
        if (RightSlider.value < 100)
        {
            RightSlider.value = RightSlider.value + 1;
        }
        else
        {
            RightSlider.value = 50;
        }

        if (LeftSlider.value > 0)
        {
            LeftSlider.value = LeftSlider.value - 1;
        }
        else
        {
            LeftSlider.value = 50;
        }
    }
}
