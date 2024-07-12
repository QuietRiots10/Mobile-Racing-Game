using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerPrefs : MonoBehaviour
{
    //Variables and Objects
    private Toggle toggle;
    public SettingsMenuScript script;
    private bool CanReset;

    private void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
    }

    public void Reset()
    {
        if (CanReset)
        {
            toggle.isOn = false;
            CanReset = false;

            //Reset all PlayerPrefs to default
            PlayerPrefs.SetInt("HighScore", 0);
            PlayerPrefs.SetInt("DoneTutorial", 0);
            PlayerPrefs.SetFloat("MusicVolume", 100);
            PlayerPrefs.SetFloat("SFXVolume", 100);

            PlayerPrefs.Save();
            script.RefreshSliders();

            Debug.Log("Reset all Player Prefs");
        }
    }

    private void OnEnable()
    {
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDisable()
    {
        toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        CanReset = value;
    }
}
