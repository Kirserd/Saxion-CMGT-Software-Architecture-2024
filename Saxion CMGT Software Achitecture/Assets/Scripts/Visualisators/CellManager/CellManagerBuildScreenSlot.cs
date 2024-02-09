using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellManagerBuildScreenSlot : MonoBehaviour
{
    [SerializeField]
    private Image _icon;
    [SerializeField]
    private TextMeshProUGUI _name;

    public void SetData(Sprite icon, string name)
    {
        _icon.sprite = icon;
        _name.text = name;
    }
}

