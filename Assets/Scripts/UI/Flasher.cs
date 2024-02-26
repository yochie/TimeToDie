using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flasher : MonoBehaviour
{
    [SerializeField]
    private float durationSeconds;
    [SerializeField] 
    private AnimationCurve flashCurve;
    [SerializeField] 
    private Image image;
    [SerializeField]
    private float curveMultiplier;

    public void Trigger()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        float startTime = Time.time;
        float timeSinceStart = 0;
        Color flashColor = this.image.color;
        while(timeSinceStart < durationSeconds)
        {
            flashColor.a = this.curveMultiplier * this.flashCurve.Evaluate(timeSinceStart / durationSeconds);
            this.image.color = flashColor;

            timeSinceStart = Time.time - startTime;
            yield return null;
        }
    }
}
