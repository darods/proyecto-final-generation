using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    private AudioSource audioSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
    }

    public float GetVolume()
    {
        return volume;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

}
