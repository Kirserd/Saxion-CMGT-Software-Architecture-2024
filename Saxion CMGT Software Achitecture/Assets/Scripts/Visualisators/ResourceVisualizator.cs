using TMPro;
using UnityEngine;

public struct ResourceDataPacket
{
    public int Money;
}
public class ResourceVisualizator : AnimatedVisualisator<ResourceDataPacket>
{
    [SerializeField]
    private TextMeshProUGUI _moneyDisplay;

    public override void DisplayData()
    {
        _moneyDisplay.text = Data.Money.ToString() + "$"; 
    }

    private void Start()
    {
        Level.Instance.Resources.OnMoneyAmountChanged += ctx =>
        {
            UpdateData(ctx);
            DisplayData();
        };

        UpdateData(new() { Money = Level.Instance.Resources.Money });
        DisplayData();
    }
    private void OnDisable()
    {
        Level.Instance.Resources.OnMoneyAmountChanged -= ctx =>
        {
            UpdateData(ctx);
            DisplayData();
        };
    }
}
