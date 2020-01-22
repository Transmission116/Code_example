using System;
public class DateTimer : ITimer
{
    private DateTime finishTime;
    private CallBackTimerAction callBackTimeAction;


    public DateTimer(string timerName, long timeSeconds, Action<object[]> handler, object[] parameters, CallBackTimerAction callBackTimeAction)
    {
        TimerName = timerName;
        finishTime = DateTime.Now.AddSeconds(timeSeconds);
        this.callBackTimeAction = callBackTimeAction;
        this.Handler = handler;
    }

    public string TimerName { get; }
    public int Index { get; set; }
    public bool Finished { get; private set; }
    public Action<object[]> Handler { get ; set; }
    public object[] Parameters { get; set ; }

    public void Update()
    {
        if (Finished) return;
        TimeSpan timeSpan = finishTime - DateTime.Now;
        callBackTimeAction?.Invoke(timeSpan);
        if (timeSpan.TotalSeconds <= 0)
        {
            Finished = true;
            Handler?.Invoke(Parameters);
        }
    }
}
