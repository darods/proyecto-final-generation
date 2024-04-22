using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    private void Awake()
    {

        GameObject canvasControllerObject = GameObject.Find("CanvasController");

        // Check if the GameObject was found
        if (canvasControllerObject != null)
        {
            // Get the CanvasController component from the GameObject
            CanvasController canvasController = canvasControllerObject.GetComponent<CanvasController>();

            // Check if the CanvasController component was found
            if (canvasController != null)
            {
                // Call the SwitchCanvas method from the CanvasController component
                
                playButton.onClick.AddListener(() =>
                {
                    canvasController.SwitchCanvas();
                    //Loader.Load(Loader.Scene.MultiplayerScene);
                });
                quitButton.onClick.AddListener(() =>
                {
                    Application.Quit();
                });

            }
            else
            {
                Debug.LogError("CanvasController component not found on GameObject named 'CanvasController'");
            }
        }
        else
        {
            Debug.LogError("GameObject named 'CanvasController' not found");
        }

        
    }
}
