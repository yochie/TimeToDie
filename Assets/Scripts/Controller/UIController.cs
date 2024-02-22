using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private StatBar healthBar;

    [SerializeField]
    private StatBar painBar;

    [SerializeField]
    private EndScreen endScreen;

    [SerializeField]
    private TextMeshProUGUI timeDisplay;

    [SerializeField]
    private Flasher damageFlasher;

    [SerializeField]
    private ScreenShaker screenShaker;

    internal void SetHealth(float current, float max)
    {
        this.healthBar.SetVal((int) Math.Round(current, MidpointRounding.AwayFromZero), (int) Math.Round(max, MidpointRounding.AwayFromZero));
    }

    internal void SetPain(float current, float max)
    {
        this.painBar.SetVal((int)Math.Round(current, MidpointRounding.AwayFromZero), (int)Math.Round(max, MidpointRounding.AwayFromZero));
    }

    internal void TriggerScreenShake()
    {
        this.screenShaker.Trigger();
    }

    internal void TriggerDamageFlash()
    {
        this.damageFlasher.Trigger();
    }

    internal void UpdateTimer(float currentTime)
    {
        this.timeDisplay.text = string.Format("{0} s",currentTime.ToString("F", CultureInfo.InvariantCulture));   
    }

    internal void EndGame(bool win, float timerSeconds, float pain, int score)
    {
        this.endScreen.Display(win, timerSeconds, (int) Math.Round(pain, MidpointRounding.AwayFromZero), score);
    }
}