public abstract class InputCallback
{
    public delegate void Callback(InputCallbackData data);
    public Callback CallbackHandler;

    public abstract void Invoke();
}
