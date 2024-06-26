﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    [SerializeField]
    private float maxPain;
    [SerializeField]
    private float maxTimeForPositiveScore;
    [SerializeField]
    private float maxPainScore;
    [SerializeField]
    private float maxTimeScore;


    internal int GetScore(float diedWithPain, float timer)
    {
        float painScore = diedWithPain.Remap(0, this.maxPain, maxPainScore, 0);
        float timeScore = timer.Remap(0, this.maxTimeForPositiveScore, this.maxTimeScore, 0);
        int score = (int) Mathf.Clamp((painScore + timeScore), 0, this.maxTimeScore + this.maxPainScore);
        return score;
    }

    internal float GetAverageSeconds()
    {
        return this.maxTimeForPositiveScore;
    }
}