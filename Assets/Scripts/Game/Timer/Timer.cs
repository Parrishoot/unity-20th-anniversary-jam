using System;
using UnityEngine;

public class Timer
{
    public float RemainingTime
    {
        get { return Mathf.Max(totalTime - currentTime, 0f); }
    }
    
    public float RemainingPercentage
    {
        get { return RemainingTime / totalTime; }
    }

    public float ElaspedPercentage
    {
        get { return Mathf.Min(1f, currentTime / totalTime); }
    }

    public float ElaspedTime
    {
        get { return Mathf.Min(totalTime, currentTime); }
    }

    public bool IsFinished
    {
        get { return currentTime >= totalTime; }
    }

    private float totalTime;

    private bool invoked = false;

    public Action OnFinishedCallback { get; private set; }

    public Timer(float totalTime, Action onFinishedCallback = null)
    {
        this.totalTime = totalTime;
        this.currentTime = 0;

        OnFinishedCallback = onFinishedCallback;
    }

    private float currentTime = 0;

    public void ProcessFrame()
    {
        currentTime += Time.deltaTime;

        if(IsFinished && !invoked)
        {
            OnFinishedCallback?.Invoke();
            invoked = true;
        }
    }
}
