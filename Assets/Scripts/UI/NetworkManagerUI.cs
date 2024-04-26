using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NetworkManagerUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI scoreText;
    public int score = 0; // Variable whose value you want to display
    public PlayerControllerForMultiplayer playerController;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerController != null && playerController.IsSpacebarPressed())
        {
            // Increment the variable value
            score++;
        }
        UpdateScore(score);*/
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }
}
