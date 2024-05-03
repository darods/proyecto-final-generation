using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour

{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;


    public static GamePauseUI Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.UnpasuedGame();

        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
       {
           SoundOptionsUI.Instance.Show();
       });
    }

    private void Start()
    {

        GameManager.Instance.OnGamePaused += Instance_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += Instance_OnGameUnpaused;

        Hide();
    }

    private void Instance_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Instance_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
