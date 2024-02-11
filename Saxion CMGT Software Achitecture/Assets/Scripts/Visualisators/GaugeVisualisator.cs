using UnityEngine;
using UnityEngine.UI;

public class GaugeVisualisator : MonoBehaviour
{
    [SerializeField] private float minValue = 0f;
    [SerializeField] private float maxValue = 1f;
    [SerializeField] private float value = 0.5f;
    [SerializeField] private Image barLevel;

    public float Value
    {
        get => value;
        set
        {
            if (value < minValue)
                value = minValue;
            else if (value > maxValue)
                value = maxValue;

            this.value = value;
            UpdateBar();
        }
    }

    private void Start() => UpdateBar();

    public void UpdateBar()
    {
        value = Mathf.Clamp(value, minValue, maxValue);

        float normalizedValue = (value - minValue) / (maxValue - minValue);

        if (barLevel != null)
            barLevel.rectTransform.localScale = new Vector3(normalizedValue, 1, 1);
    }
}