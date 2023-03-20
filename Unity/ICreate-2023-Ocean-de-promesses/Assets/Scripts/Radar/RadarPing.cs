using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float disappearTimer;
    private float disappearTimerMax;
    private Color color;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        disappearTimerMax = 1f;
        disappearTimer = 0f;
        color = new Color(1, 1, 1, 1f);
    }

    private void Update()
    {
        disappearTimer += Time.deltaTime;
        
        color.a = Mathf.Lerp(disappearTimerMax, 0f, disappearTimer / disappearTimerMax);
        var main = _particleSystem.main;
        main.startColor = color;
        
        if (disappearTimer >= disappearTimerMax)
        {
            Destroy(gameObject);
        }
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
    
    public void SetDisapperTimerMax(float disappearTimerMax)
    {
        this.disappearTimerMax = disappearTimerMax;
        disappearTimer = 0f;
    }
    
}
