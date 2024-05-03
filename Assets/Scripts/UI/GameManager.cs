using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private PlayerController playerController;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float waitingToStarTimer = 1f;
    private float countdownToStarTimer = 5f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 6f;

    private bool gamePaused = false;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        GameObject player = GameObject.Find("Player2");

        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
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
                    ChangePlayerState(false);

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStarTimer -= Time.deltaTime;
                if (countdownToStarTimer < 0f)
                {
                    state = State.GamePlaying;
                    ChangePlayerState(true);

                    gamePlayingTimer = gamePlayingTimerMax;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    ChangePlayerState(false);

                    OnStateChanged?.Invoke(this, EventArgs.Empty);

                }
                break;

            case State.GameOver:
                ChangePlayerState(false);

                break;
        }
        Debug.Log(state);
    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdownToStart;
    }

    public float GetCountdownToStartTimer()
    {
        return countdownToStarTimer;
    }

    public bool IsGameOver()
    {
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


    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void ChangePlayerState(bool newState)
    {
        playerController.SetPlayerState(newState);
    }

}
