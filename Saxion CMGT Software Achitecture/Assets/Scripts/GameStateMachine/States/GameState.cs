using MIRAI.Grid;

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

public class MapOverviewState : GameState 
{
    public override void Previous() => GameStateMachine.Next<MapOverviewState>();
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class CellSelectionState : GameState
{
    public override void Previous() => GameStateMachine.Next<MapOverviewState>();
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}

public class CellManagementState : GameState
{
    public override void Previous() => GameStateMachine.Next<CellSelectionState>();
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
        GridInteractor.Deselect();
    }
}

public class PauseState : GameState
{
    public override void Previous() => GameStateMachine.Next<MapOverviewState>();
    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
}
