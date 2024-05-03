using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Menu : MonoBehaviour
{       
    [SerializeField] private Button configurationButton;

    private GameObject tuto; 


    private void Awake()
    {
        tuto = GameObject.FindGameObjectWithTag("TutoPnl");
        tuto.SetActive(false);

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
