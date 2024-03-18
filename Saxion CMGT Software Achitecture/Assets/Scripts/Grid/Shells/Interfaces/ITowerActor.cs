namespace MIRAI.Grid.Cell.TowerActors
{
    public interface ITowerActor
    {
        TowerStats Stats { get; set; }
        ITargetSelector Selector { get; set; }
        public void Act(GridCellShell[] selection);
    }
}