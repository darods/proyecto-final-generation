using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundOptionsUI : MonoBehaviour
{

    [SerializeField] private Button closeButton;

    public static SoundOptionsUI Instance { get; private set; }


    private void Awake()
    {

        Instance = this;

        closeButton.onClick.AddListener(() =>
       {
           Hide();
       });
    }

    private void Start()
    {

        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpause;

        Hide();
    }

    private void GameManager_OnGameUnpause(object sender, EventArgs e)
    {
        Hide();
    }



    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);

    }
}
