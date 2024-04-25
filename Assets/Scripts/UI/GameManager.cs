using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 
    public static GameManager Instance { get; private set;}

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStarTimer = 1f;
    private float countdownToStarTimer= 3f;
    private float gamePlayingTimer = 500f;

    private bool gamePaused = false;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePaused)
            {
                UnpasuedGame();
            }
            else
            {
                PausedGame();
            }
        }


        switch (state)
        {
            case State.WaitingToStart:
                waitingToStarTimer -= Time.deltaTime;
                if (waitingToStarTimer < 0f)
                {
                    state = State.CountdownToStart;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStarTimer -= Time.deltaTime;
                if (countdownToStarTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GameOver:
                break;
        }

        Debug.Log(state);

    }


    public bool IsGamePlaying(){
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive(){
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer(){
        return countdownToStarTimer;
    }

    public bool IsGameOver(){
        return state == State.GameOver;
    }

    public void PausedGame()
    {
        Time.timeScale = 0; 
        gamePaused = true;
        OnGamePaused?.Invoke(this, EventArgs.Empty);
    }

    public void UnpasuedGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
        OnGameUnpaused?.Invoke(this, EventArgs.Empty);
    }
}
