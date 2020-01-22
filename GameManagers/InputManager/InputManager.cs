using System;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : IServiceInit, IServiceUpdate, IInputManager
{
    private List<InputEvent> inputHandlers;

    private int customFreeIndex = 0;


    public void Init()
    {
        inputHandlers = new List<InputEvent>();
    }

    public void Update()
    {
        HandleKeyboardInput();
    }

    public void Dispose()
    {
        inputHandlers.Clear();
    }

    public int RegisterKeyboardInputHandler(KeyCode code, Action onInputUp = null, Action onInputDown = null, Action onInput = null)
    {
        var item = new InputKeyboardEvent()
        {
            keyCode = code,
            OnInputEvent = onInput,
            OnInputDownEvent = onInputDown,
            OnInputUpEvent = onInputUp,
        };

        item.index = customFreeIndex++;

        inputHandlers.Add(item);

        return item.index;
    }

    public void UnregisterKeyboardInputHandler(int index)
    {
        var inputHandler = inputHandlers.Find(x => x.index == index);

        if (inputHandler != null)
            inputHandlers.Remove(inputHandler);
    }

    private void HandleKeyboardInput()
    {
        if (inputHandlers.Count > 0)
        {
            InputKeyboardEvent keyboardEvent;
            foreach (var item in inputHandlers)
            {
                if (item is InputKeyboardEvent)
                {
                    keyboardEvent = (InputKeyboardEvent)item;

                    if (Input.GetKey(keyboardEvent.keyCode))
                        keyboardEvent.ThrowOnInputEvent();

                    if (Input.GetKeyUp(keyboardEvent.keyCode))
                        keyboardEvent.ThrowOnInputUpEvent();

                    if (Input.GetKeyDown(keyboardEvent.keyCode))
                        keyboardEvent.ThrowOnInputDownEvent();
                }
            }
        }
    }
}

