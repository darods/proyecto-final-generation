using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button PlayButtonSinglePlayer;
    [SerializeField] private Button PlayButtonMutliPlayer;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button configurationButton;

    private GameObject tuto; 


    private void Awake()
    {
        tuto = GameObject.FindGameObjectWithTag("TutoPnl");
        tuto.SetActive(false);

        PlayButtonSinglePlayer.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("NumberOfPlayers",1);
            Loader.Load(Loader.Scene.LevelSelector);
        });
        PlayButtonMutliPlayer.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("NumberOfPlayers", 2);
            Loader.Load(Loader.Scene.LevelSelector);
        });


        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        configurationButton.onClick.AddListener(() =>
        {
            tuto.SetActive(true);
            
        });

        //Debug.Log("Puntaje actual Main Menu: " + ScoreManager.instance.GetScore());

        Time.timeScale = 1f;
    }

    void Update()
    {

        if (Input.GetKey(KeyCode.T))
        {
            tuto.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }  
}
