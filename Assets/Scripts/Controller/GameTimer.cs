using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    [SerializeField]
    private UIController ui;
    
    float currentTime;
    bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        this.currentTime = 0f;
        this.stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stopped)
        {
            this.currentTime += Time.deltaTime;
            this.ui.UpdateTimer(currentTime);
        }
    }

    public void Stop()
    {
        this.stopped = true;
    }

    public float GetTimerVal()
    {
        return this.currentTime;
    }
}
