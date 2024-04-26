using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class TestingNetcodeUi : MonoBehaviour
{
    [SerializeField] private Button startHostButton;
    [SerializeField] private Button startClientButton;

    private void Awake() {
        startHostButton.onClick.AddListener(() => {
            
            Hide();
            //
            AirplaneGameMultiplayer.Instance.StartHost();
            //Loader.LoadNetwork(Loader.Scene.MultiplayerScene);
        });

        startClientButton.onClick.AddListener(() => {
            
            Hide();
            //Loader.Load(Loader.Scene.MultiplayerScene);
            AirplaneGameMultiplayer.Instance.StartClient();
            //Loader.LoadNetwork(Loader.Scene.MultiplayerScene);
        });
    }

    private void Hide(){
        gameObject.SetActive(false);
    }
}
