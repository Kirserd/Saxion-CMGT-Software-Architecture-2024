using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class InputCallback
{
    public delegate void Callback(InputCallbackData data);
    public Callback CallbackHandler;

    public abstract void Invoke();
}
public class KeyCallback : InputCallback
{
    public KeyCode Key;
    public KeyCallback(KeyCode key) => Key = key;
    public override void Invoke()
    {
        CallbackHandler?.Invoke(new InputCallbackData
        (
            Input.GetKeyDown(Key),
            Input.GetKeyDown(Key),
            Input.GetKeyUp(Key)
        ));
    }
}
public class MouseCallback : InputCallback
{
    public int MouseButton;
    public MouseCallback(int mouseButton) => MouseButton = mouseButton;
    public override void Invoke()
    {
        CallbackHandler?.Invoke(new InputCallbackData
        (
            Input.GetMouseButtonDown(MouseButton),
            Input.GetMouseButton(MouseButton),
            Input.GetMouseButtonUp(MouseButton)
        ));
    }
}
public struct InputCallbackData
{
    public bool IsPressed;
    public bool IsDown;
    public bool IsReleased;

    public InputCallbackData(bool isPressed, bool isDown, bool isReleased)
    {
        IsPressed = isPressed;
        IsDown = isDown;
        IsReleased = isReleased;
    }
}

public static class Controls
{
    private static HashSet<InputCallback> registeredInput = new()
    {
        { new KeyCallback(KeyCode.Escape) },
        { new KeyCallback(KeyCode.Return) },
        { new MouseCallback(0) },
        { new MouseCallback(1) },
    };

    public static void Subscribe(InputCallback.Callback action, KeyCode key)
    {
        if (registeredInput.FirstOrDefault(callback => callback is 
        KeyCallback kc && kc.Key == key) is not KeyCallback keyCallback)
        {
            keyCallback = new KeyCallback(key);
            registeredInput.Add(keyCallback);
        }

        keyCallback.CallbackHandler += action;
    }
    public static void Subscribe(InputCallback.Callback action, int mouseButton)
    {
        if (registeredInput.FirstOrDefault(callback => callback is 
        MouseCallback mc && mc.MouseButton == mouseButton) is not MouseCallback mouseCallback)
        {
            mouseCallback = new MouseCallback(mouseButton);
            registeredInput.Add(mouseCallback);
        }

        mouseCallback.CallbackHandler += action;
    }

    public static void Update()
    {
        foreach (var input in registeredInput)
            input.Invoke();
    }
}
