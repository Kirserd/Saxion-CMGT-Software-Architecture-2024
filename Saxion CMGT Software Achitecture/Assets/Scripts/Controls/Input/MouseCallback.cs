using UnityEngine;

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
