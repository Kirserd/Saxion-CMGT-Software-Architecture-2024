public static class GameStateMachine 
{
    public static GameState Current { get; private set; } = new MapOverviewState();

    public static void Next(GameState state)
    {
        Current.Exit();
        Current = state;
        Current.Enter();
    }
    public static void Next<State>() where State : GameState, new()
    {
        Current.Exit();
        Current = new State();
        Current.Enter();
    }

    public static void Return() => Current.Previous();
    public static bool CurrentIs<GameState>() => Current.GetType().IsAssignableFrom(typeof(GameState));
}
