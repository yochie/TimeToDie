using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.SceneManagement;

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

    [SerializeField]
    private CanvasGroup canvasGroup;
    
    [SerializeField]
    private float fadeInDurationSeconds;

    internal void Display(bool win, float timerSeconds, float averageSeconds, int pain, int maxPain, int score)
    {
        this.header.text = win ? "Congrats, you died." : "The pain... its too much to handle.";
        this.time.text = string.Format("Time : {0} / {1} seconds", timerSeconds.ToString("F", CultureInfo.InvariantCulture), averageSeconds);
        this.pain.text = string.Format("Pain : {0} / {1} dols", pain, maxPain);
        this.score.text = string.Format("Score : {0} %", win ? score : 0);
        this.gameObject.SetActive(true);
        this.StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float ellapsedSeconds = 0;
        while (ellapsedSeconds < this.fadeInDurationSeconds)
        {
            this.canvasGroup.alpha = Mathf.Lerp(0, 1, ellapsedSeconds / this.fadeInDurationSeconds);
            ellapsedSeconds += Time.deltaTime;
            yield return null;
        }
    }

    //called by button
    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnTryAgainClicked()
    {
        SceneManager.LoadScene("Level1");
    }
}
