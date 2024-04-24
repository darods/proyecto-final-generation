using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
 
    public static GameManager Instance { get; private set;}

    public event EventHandler OnStateChanged;

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

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
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
}
