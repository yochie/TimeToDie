using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlasher : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite;

    [SerializeField]
    private float durationSeconds;

    [SerializeField]
    private Color ghostColor;

    [SerializeField]
    private AnimationCurve curve;

    private Color baseColor;

    private void Start()
    {
        this.baseColor = this.sprite.color;
    }

    internal void Trigger()
    {
        this.StartCoroutine(this.FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {

        this.sprite.color = this.ghostColor;
        float ellapsedSeconds = 0;
        while(ellapsedSeconds < this.durationSeconds)
        {
            float curveEval = this.curve.Evaluate(ellapsedSeconds / durationSeconds);
            this.sprite.color = Color.Lerp(this.ghostColor, this.baseColor, curveEval);
            ellapsedSeconds += Time.deltaTime;
            yield return null;
        }        
        this.sprite.color = this.baseColor;
    }
}
