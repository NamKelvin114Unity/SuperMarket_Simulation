using System;
using System.Collections.Generic;
using UnityEngine;

public interface IPayment
{
    public Transform HolderItem { get; }
    public bool IsCompletePayment { get; set; }
    public ItemResource HolderItemResource { get; set; }
    public List<ItemResource> ItemResources { get; set; }

    // public void CheckOrder(List<ItemResource> itemResources);
    public void PaymentValue(int totalValue, Action completePayment, Transform holderMoney, int unitPrice,
        ArrangeData arrangeData, int currentAmount);

    public void OnCompletePayment();
}