using UnityEngine;
using VirtueSky.Variables;
using Action = System.Action;

[CreateAssetMenu(menuName = "Data/BuildingData", fileName = "building_data")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private IntegerVariable priceBuildingVariable;
    [SerializeField] private Sprite iconBuy;
    [SerializeField] ArrangeData arrangeData;
    public Action onPriceChangeEvent;
    public int GetPriceBuilding => priceBuildingVariable.Value;
    public Sprite GetIconBuy => iconBuy;
    public void ResetData() => priceBuildingVariable.Value = priceBuildingVariable.InitializeValue;
    public int GetMaxNumberHorizontal => arrangeData.GetMaxNumberHorizontal;
    public int GetMaxNumberVertical => arrangeData.GetMaxNumberVertical;
    public bool IsArrangeHorizontal => arrangeData.IsArrangeHorizontal;
    public bool IsArrangeVertical => arrangeData.IsArrangeVertical;
    public bool IsArrange2D => arrangeData.IsArrange2D;

    public void OnBuying()
    {
        if (!IsBuildDone())
        {
            CoinSystem.MinusCoin(1);
            priceBuildingVariable.Value--;
            onPriceChangeEvent?.Invoke();
        }
    }

    public bool IsBuildDone() => priceBuildingVariable.Value <= 0;
}