using UnityEngine;
using VirtueSky.Inspector;

public class BuildingManager : MonoBehaviour
{
    [HeaderLine("Data")] [SerializeField] BuildingData buildingData;

    [HeaderLine("Properties")] [SerializeField]
    private EBuildingType buildingType;

    [SerializeField] private Collider collider;
    [SerializeField] private GameObject uIPrice;
    [SerializeField] Transform centerTransform;
    [SerializeField] private GameObject groupDeActive;
    [SerializeField] GameObject groupActive;
    public int GetMaxNumberHorizontal => buildingData.GetMaxNumberHorizontal;
    public int GetMaxNumberVertical => buildingData.GetMaxNumberVertical;
    public bool IsArrangeVertical => buildingData.IsArrangeVertical;
    public bool IsArrangeHorizontal => buildingData.IsArrangeHorizontal;
    public bool IsArrange2D => buildingData.IsArrange2D;

    public Transform getCenterTransform => centerTransform;

    private void Start()
    {
        buildingData.ResetData();
        UpdateBuildingStatus();
    }

    public bool IsBuyAvailable()
    {
        return buildingData.GetPriceBuilding <= CoinSystem.GetCurrentCoin() && !buildingData.IsBuildDone();
    }

    public void OnBuying()
    {
        if (IsBuyAvailable())
        {
            buildingData.OnBuying();
            if (buildingData.IsBuildDone())
            {
                buildingType = EBuildingType.Built;
                UpdateBuildingStatus();
            }
        }
    }

    void UpdateBuildingStatus()
    {
        bool isBuying = buildingType == EBuildingType.Buying;
        groupActive.SetActive(!isBuying);
        groupDeActive.SetActive(isBuying);
        uIPrice.SetActive(isBuying);
        collider.enabled = isBuying;
    }

    private void OnValidate()
    {
        UpdateBuildingStatus();
    }
}

public enum EBuildingType
{
    Buying,
    Built,
}