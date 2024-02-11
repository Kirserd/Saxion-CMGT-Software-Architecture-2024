using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MIRAI.Grid.Cell;

public class CellManagerUpgradeScreen: MonoBehaviour
{
    public TextMeshProUGUI Name, Description;
    [Space(5)]
    public TextMeshProUGUI[] StatNames;
    public GaugeVisualisator[] FinalStatGauges;
    public GaugeVisualisator[] BaseStatGauges;
    [Space(5)]
    [SerializeField]
    private Image _icon;

    [SerializeField]
    private GameObject _stats;

    public void SetIconDisplay(bool state, Sprite sprite = null) 
    {
        _icon.transform.parent.gameObject.SetActive(state);
        _icon.sprite = sprite;
    }
    public void SetStatsDisplay(bool state, TowerStats stats = null)
    {
        _stats.SetActive(state);

        if (stats == null)
            return;

        if (stats.GetType().IsAssignableFrom(typeof(OffensiveTowerStats)))
        {
            OffensiveTowerStats offensiveStats = stats as OffensiveTowerStats;

            StatNames[0].text = "VIT :";
            StatNames[1].text = "DMG :";
            StatNames[2].text = "SPD :";
            StatNames[3].text = "RNG :";

            BaseStatGauges[0].Value = offensiveStats.VIT.Base / 10f;
            BaseStatGauges[1].Value = offensiveStats.DMG.Base / 10f;
            BaseStatGauges[2].Value = offensiveStats.SPD.Base / 10f;
            BaseStatGauges[3].Value = offensiveStats.RNG.Base / 10f;

            // FinalStatGauges[0].Value = offensiveStats.VIT.Final / 10f;
            // FinalStatGauges[1].Value = offensiveStats.DMG.Final / 10f;
            // FinalStatGauges[2].Value = offensiveStats.SPD.Final / 10f;
            // FinalStatGauges[3].Value = offensiveStats.RNG.Final / 10f;
        }
    }
}

