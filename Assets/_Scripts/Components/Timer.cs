using System;
using UnityEngine;

public class Timer
{
    public Action TimerOverEvent;

    private float _maxTimer;
    private float _currentTime;

    private bool _isPlaying;

    public Timer(float maxTimer)
    {
        this._maxTimer = maxTimer;
        _isPlaying = true;
    }

    public void Start()
    {
        _isPlaying = true;
    }

    public void Stop()
    {
        _isPlaying = false; 
    }

    public void UpdateTime()
    {
        if (_isPlaying == false)
            return;

        _currentTime += Time.deltaTime;

        if (_currentTime > _maxTimer)
        {
            _currentTime = 0;
            TimerOverEvent?.Invoke();
        }
    }
}
