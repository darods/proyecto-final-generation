using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume = 1f;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GamePauseUI.Instance.OnResumeSound += GamePauseUI_OnResumeSound;
    }

    private void GamePauseUI_OnResumeSound(object sender, EventArgs e)
    {
        GamePauseUI gamePauseUI = GamePauseUI.Instance;
        PlaySound(audioClipRefSO.deliveryFail, gamePauseUI.transform.position);

    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplayer * volume);
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
    }

    public float GetVolume()
    {
        return volume;
    }
}
