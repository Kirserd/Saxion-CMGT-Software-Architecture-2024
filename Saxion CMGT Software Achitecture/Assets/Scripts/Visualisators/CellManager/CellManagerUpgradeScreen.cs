using UnityEngine;
using TMPro;

public class CellManagerUpgradeScreen: MonoBehaviour
{
    public TextMeshProUGUI Name, Description;

    [SerializeField]
    private GameObject _icon, _stats;

    public void SetIconDisplay(bool state) => _icon.SetActive(state);
    public void SetStatsDisplay(bool state) => _stats.SetActive(state);
}

