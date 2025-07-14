using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VirtueSky.Inspector;

public class UIPriceBuilding : MonoBehaviour
{
    [HeaderLine("Data")] [SerializeField] BuildingData data;

    [HeaderLine("Properties")] [SerializeField]
    private TextMeshProUGUI valueText;

    [SerializeField] private string contentText;
    [SerializeField] private Image icon;

    private void OnEnable()
    {
        data.onPriceChangeEvent += UpdateStatus;
        OnShow();
    }

    private void OnDisable()
    {
        data.onPriceChangeEvent -= UpdateStatus;
    }

    void UpdateStatus()
    {
        OnShow();
    }

    void OnShow()
    {
        valueText.text = $"{contentText}{data.GetPriceBuilding}";
        icon.sprite = data.GetIconBuy;
        icon.SetNativeSize();
    }
}