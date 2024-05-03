using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashing : MonoBehaviour
{
    public delegate void GameEndCallback(bool isWin);
    public event GameEndCallback OnGameEnd;
    [SerializeField] float mashDelay = 0.5f;
    float mashTimer;
    bool pressed;
    bool started;
    float complete = 0;
    [SerializeField] float maxComplete;
    [SerializeField] private string buttonSmash1;
    [SerializeField] private string buttonSmash2;
    private string buttonSelected;
    private bool isWin;
    private UiExample ui;

    void Start()
    {
        mashTimer = mashDelay;
        buttonSelected = buttonSmash2;
        ui = UiExample.instance;
    }

    void Update()
    {
        MashingButton();
    }

    public void StartGame()
    {
        started = true;
        
        ui.ButtonSmash();
        
    }

    private void EndGame()
    {
        ui.ResetUISmash();
        started = false;
        isWin = (complete >= maxComplete);
        complete = 0;
        mashTimer = mashDelay;
        buttonSelected = buttonSmash2;
        OnGameEnd?.Invoke(isWin);
         // Activar el evento OnGameEnd con el resultado del juego
    }

    private void DetectMash(string button)
    {
        if (Input.GetButtonDown(button) && !pressed)
        {
            ui.ButtonStar();
            complete++;
            pressed = true;
            mashTimer = mashDelay;
        }
        else if (Input.GetButtonUp(button))
        {
            ui.ButtonSmash();
            buttonSelected = buttonSelected != buttonSmash1 ? buttonSmash1 : buttonSmash2;
            pressed = false;
        }
    }

    private void MashingButton()
    {
        if (started)
        {
            mashTimer -= Time.deltaTime;
            DetectMash(buttonSelected);
            if (complete >= maxComplete)
            {
                Debug.Log("WIN");
                EndGame();
            }
            else if (mashTimer <= 0)
            {
                Debug.Log("LOSE");
                EndGame();
            }
        }
    }

    public bool GetWin()
    {
        return isWin;
    }
}
