using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI youWinLoseText;
    [SerializeField] private Button button;


    private void Awake()
    {
        button.onClick.AddListener(() =>
            {
                Loader.Load(Loader.Scene.LevelSelector);
            });
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();
            UpdateScoreText();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateScoreText()
    {
        if (recipesDeliveredText != null)
        {
            int score = ScoreManager.Instance.GetScore();
            recipesDeliveredText.text = score.ToString();

            if (ScoreManager.Instance.LevelPassed())
            {
                youWinLoseText.text += "YOU WIN!";
            }
            else
            {
                youWinLoseText.text += "YOU LOSE";
            }
        }
        else
        {
            Debug.LogError("recipesDeliveredText is not assigned in the inspector!");
        }
    }
}
