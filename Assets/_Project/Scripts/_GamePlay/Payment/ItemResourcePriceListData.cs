using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ItemResourcePriceListData", fileName = "item_resource_price_list_data")]
public class ItemResourcePriceListData : ScriptableObject
{
    [SerializeField] private int unitPrice;
    [SerializeField] private List<ItemResourcePrice> itemResourcePrices;

    public int CheckOrder(EItemType itemType)
    {
        return itemResourcePrices.FirstOrDefault(i => i.GetItemType == itemType).GetPrice;
    }

    public int GetUnitPrice() => unitPrice;
}

[Serializable]
public class ItemResourcePrice
{
    [SerializeField] private EItemType itemType;
    [SerializeField] private int price;
    public EItemType GetItemType => itemType;
    public int GetPrice => price;
}