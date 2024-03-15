using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class Controls
{
    private static HashSet<InputCallback> _registeredInput = new()
    {
        { new KeyCallback(KeyCode.Escape) },
        { new KeyCallback(KeyCode.Return) },
        { new MouseCallback(0) },
        { new MouseCallback(1) },
    };

    public static void Subscribe(InputCallback.Callback action, KeyCode key)
    {
        if (_registeredInput.FirstOrDefault(callback => callback is 
        KeyCallback kc && kc.Key == key) is not KeyCallback keyCallback)
        {
            keyCallback = new KeyCallback(key);
            _registeredInput.Add(keyCallback);
        }

        keyCallback.CallbackHandler += action;
    }
    public static void Subscribe(InputCallback.Callback action, int mouseButton)
    {
        if (_registeredInput.FirstOrDefault(callback => callback is 
        MouseCallback mc && mc.MouseButton == mouseButton) is not MouseCallback mouseCallback)
        {
            mouseCallback = new MouseCallback(mouseButton);
            _registeredInput.Add(mouseCallback);
        }

        mouseCallback.CallbackHandler += action;
    }

    public static void Update()
    {
        foreach (var input in _registeredInput)
            input.Invoke();
    }
}
