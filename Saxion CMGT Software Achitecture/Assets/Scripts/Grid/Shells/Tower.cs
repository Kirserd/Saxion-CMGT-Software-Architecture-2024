namespace MIRAI.Grid.Cell
{
    public class Tower : GridCellShell
    {
        private ITowerActor _actor;
        protected override void Awake() { }
    }

    public interface ITowerActor
    {
        ITargetSelector Selector { get; set; }
        public void Act(SelectionResults selection);
    }

    public struct SelectionResults
    {
        public GridCellShell[] Results;

        public SelectionResults(GridCellShell[] results) 
            => Results = results;
    }

    public interface ITargetSelector
    {
        public SelectionResults GetSeletion();
    }

    public class GeneralStats
    {
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MinHP { get; set; }
    }

    public class TowerStats : GeneralStats
    {

    }

    public class CreatureStats : GeneralStats
    {

    }
}