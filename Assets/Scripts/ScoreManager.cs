using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string ScoreKey = "PlayerScore";
    private int playerScore = 0;
    public static ScoreManager Instance { get; private set; }


    public static ScoreManager instance;

    private bool levelPassed = false;

    private void Awake()
    {
        Instance = this;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Cargar el puntaje guardado al inicio del juego
        playerScore = PlayerPrefs.GetInt(ScoreKey, 0);
    }

    public void AddPoints(int points)
    {
        playerScore += points;
        // Guardar el puntaje actual
        SaveScore();
    }

    public void DeductPoints(int points)
    {
        playerScore -= points;
        // Asegurarse de que el puntaje no sea negativo
        playerScore = Mathf.Max(0, playerScore);
        // Guardar el puntaje actual
        SaveScore();
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void ResetScore()
    {
        playerScore = 0;
        // Eliminar el puntaje guardado
        PlayerPrefs.DeleteKey(ScoreKey);
    }

    private void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, playerScore);
        PlayerPrefs.Save();
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
        if (playerScore >= 50)
        {
            levelPassed = true;
        }
    }

    public bool LevelPassed()
    {
        return levelPassed;
    }
}
