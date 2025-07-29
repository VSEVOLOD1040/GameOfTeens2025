using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;
    public GameObject settings;

    public Slider music;
    public Slider sfx;
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    public void Settings()
    {
        settings.SetActive(true);
        menu.SetActive(false);

        music.value = PlayerPrefs.GetFloat("VolumeMusic", 1);
        sfx.value = PlayerPrefs.GetFloat("VolumeSFX", 1);
    }
    public void LeaveSettings()
    {
        settings.SetActive(false);
        menu.SetActive(true);

        PlayerPrefs.SetFloat("VolumeMusic", music.value);
        PlayerPrefs.SetFloat("VolumeSFX", sfx.value);

    }

    public void Exit()
    {
        Application.Quit();
    }

}
