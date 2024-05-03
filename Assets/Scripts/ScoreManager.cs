using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private const string ScoreKey = "PlayerScore";
    private int playerScore = 0;
    private const string Level1CompletedKey = "Level1Completed";
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


    public void AddPoints(int points)
    {
        playerScore += points;
        // Guardar el puntaje actual
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

        // Verifica si el jugador ha alcanzado el puntaje requerido para completar el nivel 1
        if (playerScore >= 300)
        {
            PlayerPrefs.SetInt(Level1CompletedKey, 1);
            PlayerPrefs.Save();
            levelPassed = true;
        }
    }

    public bool IsLevel1Completed()
    {
        return PlayerPrefs.GetInt(Level1CompletedKey, 0) == 1;
    }
    public bool LevelPassed()
    {
        Debug.Log("level pasado: " + levelPassed);
        return levelPassed;
    }
   
}
