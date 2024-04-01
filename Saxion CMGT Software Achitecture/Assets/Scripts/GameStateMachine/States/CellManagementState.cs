using MIRAI.Grid;

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
