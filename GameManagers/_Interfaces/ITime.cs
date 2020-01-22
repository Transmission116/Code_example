
using System;
public interface ITimer
{
    Action<object[]> Handler { get; set; }
    object[] Parameters { get;}
    bool Finished { get; }
    int Index { get; set; }
    void Update();
}