using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMashing : MonoBehaviour
{
    [SerializeField] float mashDelay = .5f;
    float mash;
    bool pressed;
    bool started;

    float complete = 0;
    [SerializeField] float maxComplete;
    [SerializeField] private string buttonSmash1;
    [SerializeField] private string buttonSmash2;
    private string buttonSelected;
    private bool win;
    private UiExample ui;
    void Start()
    {
        mash = mashDelay;
        buttonSelected = buttonSmash2;
        ui = UiExample.instance;
    }
    void Update()
    {
        MashingButton();
    }

    public bool StartButtonMashWithDelay(float delay)
    {
        StartCoroutine(StartWithDelay(delay));
        return win;
    }
    private IEnumerator StartWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartButtonMash();
    }
    public void StartButtonMash()
    {
        ui.ButtonSmash();
        started = true;

    }
    private void EndGame()
    {
        ui.ResetUISmash();
        complete = 0;
        started = false;
        
    }
    private void MashingButton()
    {
        if (started)
        {

            mash -= Time.deltaTime;
            DetectMash(buttonSelected);
            if (complete >= maxComplete)
            {
                Debug.Log("WIN");
                win = true;
                EndGame();
            }
            else if (mash <= 0)
            {
                Debug.Log("LOSE");
                EndGame();
            }
        }
    }
    private void DetectMash(string button)
    {


        if (Input.GetButtonDown(button) && !pressed)
        {
            ui.ButtonStar();
            complete++;
            pressed = true;
            mash = mashDelay;

        }
        else if (Input.GetButtonUp(button))
        {
            ui.ButtonSmash();

            buttonSelected = buttonSelected != buttonSmash1 ? buttonSmash1 : buttonSmash2;
            pressed = false;
        }
    }


}
