using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer: ITimer
{
    private float time, currentTime;
    private bool loop;

    public Action<object[]> Handler { get; set; }
    public object[] Parameters { get ; set; }
    public bool Finished { get; private set; }
    public int Index { get; set; }

    public Timer(Action<object[]> handler, object[] parameters, float time, bool loop)
    {
        this.time = time;
        this.loop = loop;
        currentTime = time;
        Handler = handler;
        Parameters = parameters;
        Finished = false;
    }

    public void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            Handler?.Invoke(Parameters);
            if (loop)
                currentTime = time;
            else
                Finished = true;
        }
    }
}