using MIRAI.Grid.Cell;
using UnityEngine;

public class CellManagerVisualisator : Visualisator<CellManagerDataPacket>, ISubscriber
{
    [SerializeField]
    private CellManagerUpgradeScreen _upgradeScreen;
    [SerializeField]
    private CellManagerBuildScreen _buildScreen;
    [SerializeField]
    private GameObject _sidebar;

    public override void DisplayData()
    {
        if (Data.Selection is null)
            return;

        if (Data.Current == CellManagerDataPacket.Screen.Upgrade)
            DisplayUpgradeScreen();
        else 
            DisplayBuildScreen();
    }
    private void DisplayUpgradeScreen()
    {
        _upgradeScreen.gameObject.SetActive(true);

        if (Data.Selection.GetType().IsAssignableFrom(typeof(OpenCell)))
        {
            _upgradeScreen.SetIconDisplay(false);
            _upgradeScreen.SetStatsDisplay(false);
        }

        _upgradeScreen.Name.text = Data.Selection.ShellData.Name;
        _upgradeScreen.Description.text = Data.Selection.ShellData.Description;
    }
    private void DisplayBuildScreen()
    {
        _buildScreen.gameObject.SetActive(true);
    }

    public void ChangeScreenTo(int screenId)
    {
        Data.Current = (CellManagerDataPacket.Screen)screenId;
        Hide();
        Open();
    }

    protected virtual void Awake() => Subscribe();

    public virtual void Subscribe()
    {
        GameState.OnEnter += OnGameStateEnter;
        GameState.OnExit += OnGameStateExit;
    }
    public virtual void Unsubscribe()
    {
        GameState.OnEnter -= OnGameStateEnter;
        GameState.OnExit -= OnGameStateExit;
    }

    private void OnGameStateEnter(GameState state)
    {
        if (state is CellManagementState)
            Open();
    }
    private void OnGameStateExit(GameState state)
    {
        if (state is CellManagementState)
            Hide();
    }

    public override void Open() 
    {
        _sidebar.SetActive(true);
        DisplayData();
    }
    public override void Hide()
    {
        _sidebar.SetActive(false);
        _upgradeScreen.gameObject.SetActive(false);
        _buildScreen.gameObject.SetActive(false);
    }

    protected virtual void OnDestroy() => Unsubscribe();
}