namespace MIRAI.Grid.Cell
{
    public interface ITowerActor
    {
        ITargetSelector Selector { get; set; }
        public void Act(GridCellShell[] selection);
    }
}