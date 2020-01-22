using System;
using UnityEngine;

public class InputEvent
{
    public int index;

    public Action OnInputUpEvent;
    public Action OnInputDownEvent;
    public Action OnInputEvent;

    public void ThrowOnInputUpEvent()
    {
        if (OnInputUpEvent != null)
            OnInputUpEvent();
    }

    public void ThrowOnInputDownEvent()
    {
        if (OnInputDownEvent != null)
            OnInputDownEvent();
    }

    public void ThrowOnInputEvent()
    {
        if (OnInputEvent != null)
            OnInputEvent();
    }
}

public class InputKeyboardEvent : InputEvent
{
    public KeyCode keyCode;
}

public class InputMouseEvent : InputEvent
{
    public int mouseCode;
}