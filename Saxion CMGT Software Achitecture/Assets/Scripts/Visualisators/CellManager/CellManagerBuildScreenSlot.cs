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
    private TextMeshProUGUI _name;

    public void SetData(TowerBlueprint blueprint)
    {
        _icon.sprite = blueprint.Tooltip.Icon;
        _name.text = blueprint.Tooltip.Name;
        Blueprint = blueprint;
    }
    
    public void TryToBuy()
    {
        GridCellShell selected = GridInteractor.Selection;
        Tower tower = TowerBuilder.BuildTower(Blueprint);
        
        if (GridRegistrar.TryRegisterAt(selected.X, selected.Y, tower))
            return;

        Destroy(tower.gameObject);
    }
}

