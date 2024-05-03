using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EarthUI : MonoBehaviour
{
    [SerializeField] private Button MainMenuButton;



    private void Awake()
    {

        MainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });


    }
}
