using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Inspector;

public class ResourceSellerManager : MonoBehaviour, IResource
{
    [SerializeField] ResourceSellerData resourceSellerData;
    [ReadOnly] [SerializeField] List<ItemResource> currentResourceList = new List<ItemResource>();
    [SerializeField] private Transform resourceHolder;
    [SerializeField] SpriteRenderer icon;

    public bool ArrangeHorizontal => resourceSellerData.ArrangeHorizontal;
    public bool ArrangeVertical => resourceSellerData.ArrangeVertical;

    public bool Arrange2D => resourceSellerData.Arrange2D;
    public Transform ResourceHolder => resourceHolder;
    public int ResourceCount => currentResourceList.Count;

    public int GetMaxCapacityHorizontal => resourceSellerData.GetMaxCapacityHorizontal;

    public int GetMaxCapacityVertical => resourceSellerData.GetMaxCapacityVertical;

    private void Start()
    {
        resourceSellerData.ResetData();
        icon.sprite = resourceSellerData.Icon;
    }

    public bool IsCanAddResource(EItemType itemType)
    {
        return resourceSellerData.IsCanAddResource((itemType)) &&
               currentResourceList.Count < resourceSellerData.GetMaxValueResource;
    }

    public ItemResource GetResource()
    {
        ItemResource getItem = null;
        if (currentResourceList.Count <= 0) return getItem;
        getItem = currentResourceList[^1];
        if (getItem)
        {
            currentResourceList.Remove(getItem);
            resourceSellerData.RemoveItem(new ItemCollectData(getItem.ItemType, getItem.Price));
        }

        return getItem;
    }

    public void AddResource(ItemResource resource)
    {
        currentResourceList.Add(resource);
        resourceSellerData.AddItem(new ItemCollectData(resource.ItemType, resource.Price));
    }
}