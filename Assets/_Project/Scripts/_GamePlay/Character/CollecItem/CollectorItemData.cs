using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VirtueSky.Inspector;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/CollectorItemData", fileName = "collector_item_data")]
public class CollectorItemData : ScriptableObject
{
    [SerializeField] private bool isRandomCapacity;
    [SerializeField] ArrangeData arrangeData;

    [ShowIf(nameof(isRandomCapacity), false)] [SerializeField]
    int maxCapacity;

    [ShowIf(nameof(isRandomCapacity))] [SerializeField]
    int maxRange;


    [SerializeField] private List<EItemType> itemTypeCollectableList = new List<EItemType>();
    [SerializeField] private List<ItemCollectData> itemCollectData = new List<ItemCollectData>();
    public Action onItemCollectUpdateEvent;
    public ArrangeData ArrangeData => arrangeData;

    private int _currentMaxCapacity;


    public void ResetData()
    {
        if (isRandomCapacity) _currentMaxCapacity = Random.Range(1, maxRange);
        else _currentMaxCapacity = maxCapacity;
        itemCollectData.Clear();
    }

    public bool IsCanCollectItem(EItemType itemType)
    {
        return itemTypeCollectableList.Contains(itemType);
    }

    public int GetCurrentItemAmount => itemCollectData.Count;

    public void AddItemResource(ItemCollectData itemCollect)
    {
        itemCollectData.Add(itemCollect);
        onItemCollectUpdateEvent?.Invoke();
    }

    public void RemoveItemResource(ItemCollectData itemCollect)
    {
        var findItem =
            itemCollectData.FirstOrDefault(i => i.itemType == itemCollect.itemType && i.price == itemCollect.price);
        if (findItem != null)
        {
            itemCollectData.Remove(findItem);
        }

        onItemCollectUpdateEvent?.Invoke();
    }

    public int MaxCapacity()
    {
        return _currentMaxCapacity;
    }

    public int GetCurrentItemMissionAmount(List<EItemType> itemTypeMission)
    {
        int count = 0;
        foreach (var item in itemCollectData)
        {
            if (itemTypeMission.Contains(item.itemType))
            {
                count++;
            }
        }

        return count;
    }
}

[Serializable]
public class ItemCollectData
{
    public EItemType itemType;
    public int price;

    public ItemCollectData(EItemType itemType, int price)
    {
        this.itemType = itemType;
        this.price = price;
    }
}