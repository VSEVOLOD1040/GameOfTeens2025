using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public List<GameObject> ObjectsToDisableWhenPaused = new List<GameObject>();
    public GameObject PauseObject;

    public Slider music;
    public Slider sfx;
    public void Pause()
    {
        foreach (GameObject obj in ObjectsToDisableWhenPaused)
        {
            obj.SetActive(false);
        }
        PauseObject.SetActive(true);

        music.value = PlayerPrefs.GetFloat("VolumeMusic", 1);
        sfx.value = PlayerPrefs.GetFloat("VolumeSFX", 1);

        Time.timeScale = 0;
    }
    public void Unpause()
    {
        Time.timeScale = 1;

        PlayerPrefs.SetFloat("VolumeMusic", music.value);
        PlayerPrefs.SetFloat("VolumeSFX", sfx.value);

        PauseObject.SetActive(false);
        foreach (GameObject obj in ObjectsToDisableWhenPaused)
        {
            obj.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (!PauseObject.activeSelf)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }
}
