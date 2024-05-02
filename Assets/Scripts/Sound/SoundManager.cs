using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioClipRefSO audioClipRefSO;

    private float volume = 15f;

    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        NPCController.Instance.OnOrderDeliveredSound += NPCController_OnOrderDelivered;
        Debug.Log("Volumen :" + volume);
    }

    private void NPCController_OnOrderDelivered(object sender, EventArgs e)
    {
        NPCController nPCController = NPCController.Instance;
        PlaySound(audioClipRefSO.deliveryFail, nPCController.transform.position);
    }


    public void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplayer = 1f)
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
