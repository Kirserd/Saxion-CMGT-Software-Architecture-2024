using MIRAI.Grid;
using MIRAI.Grid.Cell;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellManagerBuildScreenSlot : MonoBehaviour
{
    public TowerBlueprint Blueprint { get; private set; }
 
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _nameEntry;
    [SerializeField]
    private TextMeshProUGUI _costEntry;

    private int _cost;

    public void SetData(TowerBuildingShopEntry entry)
    {
        _cost = entry.Cost;
        _costEntry.text = entry.Cost + "$";

        _icon.sprite = entry.Blueprint.Tooltip.Icon;
        _nameEntry.text = entry.Blueprint.Tooltip.Name;
        Blueprint = entry.Blueprint;
    }
    
    public void TryToBuy()
    {
        GridCellShell selected = GridInteractor.Selection;

        if(!GridRegistrar.IsEmptyAt(selected.X, selected.Y) || !Level.Instance.Resources.TryWithdrawMoney(_cost))
        {
            //TODO: VISUALIZE;
            return;
        }
        GridRegistrar.TryRegisterAt(selected.X, selected.Y, TowerBuilder.BuildTower(Blueprint));
    }
}
