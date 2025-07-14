using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ResourceSellerData", fileName = "resource_seller_data")]
public class ResourceSellerData : ScriptableObject
{
    [SerializeField] private int maxValueResourceVariable;

    [SerializeField] private Sprite icon;
    [SerializeField] ArrangeData arrangeData;
    [SerializeField] List<EItemType> itemTypeSellList = new List<EItemType>();
    [SerializeField] List<ItemCollectData> currentCollectData = new List<ItemCollectData>();
    public Sprite Icon => icon;

    public bool IsCanAddResource(EItemType itemType) => itemTypeSellList.Contains(itemType);

    public void ResetData()
    {
        currentCollectData.Clear();
    }

    public void AddItem(ItemCollectData item)
    {
        currentCollectData.Add(item);
    }

    public void RemoveItem(ItemCollectData item)
    {
        var findItem = currentCollectData.FirstOrDefault(i => i.itemType == item.itemType && i.price == item.price);
        if (findItem != null) currentCollectData.Remove(findItem);
    }

    public int GetMaxValueResource => maxValueResourceVariable;
    public int GetMaxCapacityHorizontal => arrangeData.GetMaxNumberHorizontal;
    public int GetMaxCapacityVertical => arrangeData.GetMaxNumberHorizontal;
    public bool ArrangeHorizontal => arrangeData.IsArrangeHorizontal;
    public bool ArrangeVertical => arrangeData.IsArrangeVertical;

    public bool Arrange2D => arrangeData.IsArrange2D;
}