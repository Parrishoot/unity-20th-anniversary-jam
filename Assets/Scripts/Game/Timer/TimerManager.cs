using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    private static List<Timer> activeTimers = new List<Timer>();

    private static List<Timer> timersToAdd = new List<Timer>();

    private static List<Timer> timersToRemove = new List<Timer>();

    public static Timer RegisterTimer(float time, Action onTimerFinishedCallback = null)
    {
        Timer newTimer = new Timer(time, onTimerFinishedCallback);
        timersToAdd.Add(newTimer);

        return newTimer;
    }

    public static void DeregisterTimer(Timer timer)
    {
        timersToRemove.Add(timer);
    }

    private void Update()
    {
        foreach (Timer timer in activeTimers)
        {
            timer.ProcessFrame();
        }

        activeTimers.RemoveAll(x => x.IsFinished);
    }

    private void LateUpdate()
    {
        activeTimers.AddRange(timersToAdd);
        activeTimers.RemoveAll(x => timersToRemove.Contains(x));

        timersToAdd = new List<Timer>();
        timersToRemove = new List<Timer>();
    }
}
