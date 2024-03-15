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
