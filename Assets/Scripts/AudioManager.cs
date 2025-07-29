using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource sound;


    private int lastTrackIndex = -1;
    private System.Random rng = new System.Random();

    void Start()
    {
        if (music.Length > 0)
        {
            PlayNextRandomTrack();
        }
    }

    void Update()
    {
        sound.volume = PlayerPrefs.GetFloat("VolumeMusic", 1f);
        if (!sound.isPlaying)
        {
            PlayNextRandomTrack();
        }
        UpdateSFXVolume();
    }
    void UpdateSFXVolume()
    {
        float sfxVolume = PlayerPrefs.GetFloat("VolumeSFX", 1f);

        AudioSource[] allSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allSources)
        {
            if (source == sound)
                continue;

            Transform current = source.transform;
            bool isUnderGameController = false;

            while (current != null)
            {
                if (current.CompareTag("GameController"))
                {
                    isUnderGameController = true;
                    break;
                }
                current = current.parent;
            }

            if (!isUnderGameController)
            {
                source.volume = sfxVolume;
            }
        }
    }
    void PlayNextRandomTrack()
    {
        if (music.Length == 0)
            return;

        int nextTrackIndex;

        do
        {
            nextTrackIndex = rng.Next(music.Length);
        } while (music.Length > 1 && nextTrackIndex == lastTrackIndex);
        lastTrackIndex = nextTrackIndex;

        sound.clip = music[nextTrackIndex];
        sound.Play();
    }
}
