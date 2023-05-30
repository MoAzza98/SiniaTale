using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineShake : MonoBehaviour
{
    public static CinemachineShake Instance { get; private set; }

    private CinemachineVirtualCamera vCamShake;
    private float shakeTimer;
    private float startingIntensity;
    private float shakeTimerTotal;

    void Awake()
    {
        Instance = this;
        vCamShake = GetComponent<CinemachineVirtualCamera>();
    }
    public void ShakeCamera(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin perlinNoise = vCamShake.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        perlinNoise.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        shakeTimerTotal = time;
        shakeTimer = time;

    }

    void Update()
    {
        if(shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;

            CinemachineBasicMultiChannelPerlin perlinNoise = vCamShake.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            perlinNoise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, 1 - (shakeTimer / shakeTimerTotal));
        }
    }


}
