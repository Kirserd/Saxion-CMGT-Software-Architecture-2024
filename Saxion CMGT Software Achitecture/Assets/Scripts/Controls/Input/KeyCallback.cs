using UnityEngine;

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
