using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VirtueSky.Variables;

[CreateAssetMenu(menuName = "Data/ResourceSupplierData", fileName = "resource_supplier_data")]
public class ResourceSupplierData : ScriptableObject
{
    [Unity.Collections.ReadOnly] [SerializeField]
    private int maxValueResourceVariable;

    [SerializeField] private float timeSpawnResourceVariable;
    [SerializeField] private ItemResource itemResource;
    [SerializeField] List<ItemCollectData> currentCollectData = new List<ItemCollectData>();

    public void AddItem(ItemCollectData item)
    {
        if (item.itemType == itemResource.ItemType)
        {
            currentCollectData.Add(item);
        }
    }

    public void RemoveItem(ItemCollectData item)
    {
        var findItem = currentCollectData.FirstOrDefault(i => i.itemType == item.itemType && i.price == item.price);
        if (findItem != null) currentCollectData.Remove(findItem);
    }

    public float GetTimeSpawnResourceValue => timeSpawnResourceVariable;
    public ItemResource GetItemResourceValue => itemResource;
    public int GetMaxValueResource => maxValueResourceVariable;
}