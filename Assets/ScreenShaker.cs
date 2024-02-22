using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera vcam;
    [SerializeField] 
    private float noiseAmplitude;
    [SerializeField] 
    private float noiseFrequency;
    [SerializeField] 
    private float durationSeconds;

    private CinemachineBasicMultiChannelPerlin vcamNoise;

    private void Awake()
    {
        this.vcamNoise = this.vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    internal void Trigger()
    {
        this.StopAllCoroutines();
        this.StartCoroutine(this.ShakeCoroutine());        
    }

    private IEnumerator ShakeCoroutine()
    {
        this.vcamNoise.m_AmplitudeGain = this.noiseAmplitude;
        this.vcamNoise.m_FrequencyGain = this.noiseFrequency;
        yield return new WaitForSeconds(this.durationSeconds);
        this.vcamNoise.m_AmplitudeGain = 0;
        this.vcamNoise.m_FrequencyGain = 0;
    }
}
