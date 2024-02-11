using MIRAI.Grid.Cell;
using UnityEngine;

public class CellManagerBuildScreen : MonoBehaviour
{
    [SerializeField]
    private CellManagerBuildScreenSlot _slotPrefab;
    [SerializeField]
    private Transform _slotContainer;

    private bool _isInitialized = false;
    public void TryInitSlots(TowerBlueprint[] slots)
    {
        if (_isInitialized)
            return;

        foreach (var slot in slots)
            Instantiate(_slotPrefab, _slotContainer).SetData(slot);

        _isInitialized = true;
    }
}

