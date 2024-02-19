using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class EndScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI header;

    [SerializeField]
    private TextMeshProUGUI time;

    [SerializeField]
    private TextMeshProUGUI pain;

    [SerializeField]
    private TextMeshProUGUI score;

    internal void Display(bool win, float timerSeconds, int pain, int score)
    {
        this.header.text = win ? "Congrats, you died." : "Its too much to handle. You live to die another day.";
        this.time.text = string.Format("Time : {0} seconds", timerSeconds.ToString("F", CultureInfo.InvariantCulture));
        this.pain.text = string.Format("Pain : {0} dols", pain);
        this.score.text = string.Format("Score : {0} %", win ? score : 0);
        this.gameObject.SetActive(true);
    }
}
