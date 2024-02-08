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

    internal void SetHealth(int current, int max)
    {
        this.healthBar.SetVal(current, max);
    }

    internal void SetPain(int current, int max)
    {
        this.painBar.SetVal(current, max);
    }

    internal void UpdateTimer(float currentTime)
    {
        this.timeDisplay.text = string.Format("{0} s",currentTime.ToString("F", CultureInfo.InvariantCulture));   
    }

    internal void EndGame(bool win, float timerSeconds, int pain, int score)
    {
        this.endScreen.Display(win, timerSeconds, pain, score);
    }
}