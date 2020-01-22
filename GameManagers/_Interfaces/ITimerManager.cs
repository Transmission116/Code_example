using System;

public interface ITimerManager
{
    void StopTimer(Action<object[]> handler);
    void AddTimer(Action<object[]> handler, object[] parameters = null, float time = 1, bool loop = false);
    void AddTimer(string timerName, long timeSeconds, Action<object[]> handler, object[] parameters = null, CallBackTimerAction leftTimeCallBack = null);
}