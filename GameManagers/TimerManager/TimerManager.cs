using UnityEngine;
using System;
using System.Collections.Generic;

public delegate void CallBackTimerAction(TimeSpan finishTime);
public class TimerManager : IServiceInit,IServiceUpdate, ITimerManager
{
    private int timersCount = 0;

    private List<ITimer> timers;

    private static object sync = new object();

    public void Init()
    {
        timers = new List<ITimer>();
    }

    public void Dispose()
    {
        timers.Clear();
    }



    public void StopTimer(Action<object[]> handler)
    {
        for (int i = 0; i < timers.Count; i++)
            if ((timers[i]).Handler == handler)
                timers.RemoveAt(i);
    }

    public void StopTimer(int index)
    {
        var timer = timers.Find((x) => x.Index == index);

        if (timers.Contains(timer))
            timers.Remove(timer);
    }

    public void StopTimerByName(string timerName)
    {
        for (int i = 0; i < timers.Count; i++)
        {
            if (timers[i] is DateTimer)
            {
                if ((timers[i] as DateTimer).TimerName == timerName)
                {
                    timers.Remove(timers[i]);
                    break;
                }
            }
        }
    }

    public void AddTimer(Action<object[]> handler, object[] parameters = null, float time = 1, bool loop = false)
    {
        Timer timer = new Timer(handler, parameters, time, loop);
        timer.Index = timersCount++;
        timers.Add(timer);
    }
    public void AddTimer(string timerName, long timeSeconds,  Action<object[]> handler, object[] parameters = null, CallBackTimerAction leftTimeCallBack = null)
    {
        DateTimer dateTimer = new DateTimer(timerName, timeSeconds, handler, parameters, leftTimeCallBack);
        dateTimer.Index = timersCount;
        timers.Add(dateTimer);
    }

    public void Update()
    {
        lock (sync)
        {
            for (int i = 0; i < timers.Count; i++)
            {
                ProfileAssistant.DebugLog("TimerActive");
                if (timers[i] != null)
                {
                    if (timers[i].Finished)
                        timers.RemoveAt(i);
                    else
                        timers[i].Update();
                }
            }
        }
    }

    public static string FormatTime(TimeSpan _timeSpan)
    {
        string _timeText;

        if (_timeSpan.TotalHours >= 1)
        {
            _timeText = string.Format("{0:00}:{1:00}:{2:00}", _timeSpan.TotalHours, _timeSpan.Minutes, _timeSpan.Seconds);
        }
        else
        {
            _timeText = string.Format("{0:00}:{1:00}", _timeSpan.Minutes, _timeSpan.Seconds);
        }
        return _timeText;
    }


}

