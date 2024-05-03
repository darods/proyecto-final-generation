using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    [Header("Players")]
    [SerializeField] private PlayerSpawn playerSpawn;
    [SerializeField] private PlayerController player1Controller;
    [SerializeField] private PlayerController player2Controller;

    [Header("Pilot")]
    [SerializeField] private Pilot pilot;


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
    private float gamePlayingTimerMax = 150f;

    private bool gamePaused = false;


    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Start()
    {
        StartCoroutine(WaitToGetPlayersInScene());
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
                    ChangePlayerState(false);

                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.CountdownToStart:
                countdownToStarTimer -= Time.deltaTime;
                if (countdownToStarTimer < 0f)
                {
                    state = State.GamePlaying;
                    pilot.StartSleepDelay();
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
        if (player1Controller != null)
        {
            player1Controller.SetPlayerState(newState);
        }
        if (player2Controller != null)
        {
            player2Controller.SetPlayerState(newState);
        }
    }

    private IEnumerator WaitToGetPlayersInScene()
    {
        yield return new WaitForSeconds(1);
        player1Controller = playerSpawn.ReturnPlayer1Spawned();
        player2Controller = playerSpawn.ReturnPlayer2Spawned();
    }
}
