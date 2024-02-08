using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UIController ui;

    [SerializeField]
    private GameTimer timer;

    [SerializeField]
    private ScoreCalculator scorer;

    public void EndGame(bool died, int pain)
    {
        this.timer.Stop();
        float timer = this.timer.GetTimerVal();
        int score = this.scorer.GetScore(pain, timer);
        this.ui.EndGame(win: died, timer, pain, score);
    }
}
