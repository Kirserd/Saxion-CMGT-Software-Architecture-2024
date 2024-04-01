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
