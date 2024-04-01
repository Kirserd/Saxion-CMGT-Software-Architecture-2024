public abstract class GameState
{
    public abstract void Previous();

    public delegate void OnEnterState(GameState state);
    public delegate void OnExitState(GameState state);

    public static OnEnterState OnEnter;
    public static OnExitState OnExit;

    public virtual void Enter() => OnEnter.Invoke(this);
    public virtual void Exit() => OnExit.Invoke(this);
}
