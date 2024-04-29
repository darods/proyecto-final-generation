using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button configurationButton;

    private GameObject tuto; 


    private void Awake()
    {
        tuto = GameObject.FindGameObjectWithTag("TutoPnl");
        tuto.SetActive(false);

        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        configurationButton.onClick.AddListener(() =>
        {
            tuto.SetActive(true);
            
        });


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
