using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class inGame_readNodeDetermination
{
    public inGame_Track inGame_Track;
    public ButtonTap ButtonTap;
    private string ButtonType;//for SFX 撥放
    private string DeterminationType;
    private float countdownTimer;
    private bool isTrigger = false;

    public async void StartCountDown()
    {
        if (countdownTimer <= 0)
        {
            Debug.LogError("Not Set \"countdownTimer\" yet");
        }
        else
        {
            await CountDown();
        }
        
    }

    async Task CountDown()
    {
        while(countdownTimer >= -0.150 && isTrigger == false)
        {
            countdownTimer -= Time.deltaTime;
            await Task.Yield();
        }
        if(isTrigger == false)
        {
            inGame_Track.OnDeterminationTimeout();
            SetHaveBeenTrigger();
            //Debug.Log("Time out");
        }
    }

    public bool isDeterminable()//改回傳String
    {
        if (countdownTimer <= 0.150 && countdownTimer >= -0.150)//150ms
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool isMyTypeAcceptable(string inputType)
    {
        if(DeterminationType != null)
        {
            if (inputType == DeterminationType) 
            { return true; }
            else { return false; }
        }
        else
        {
            Debug.LogError("Not set \"DeterminationType\" yet");
            return false;
        }
    }

    public float GetNowTimerTime()
    {
        return countdownTimer;
    }

    public void SetHaveBeenTrigger()
    {
        isTrigger = true;
        ButtonTap.DisableThisButton();
    }

    public void SetDeterminationType(string type)
    {
        DeterminationType = type;
    }
    public string GetDeterminationType()
    {
        return DeterminationType;
    }
    public void SetButtonType(string type)
    {
        ButtonType = type;
    }
    public string GetButtonType()
    {
        return ButtonType;
    }

    public void SetCountdownTimer(float DetermiTime)
    {
        countdownTimer = DetermiTime;
    }
}
