using System;
using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Inspector;

public class NPCInteractArea : CharacterInteractArea, IPayment
{
    [HeaderLine("Core")] [SerializeField] private NPC npc;

    public Transform HolderItem => resourceHolder;
    public bool IsCompletePayment { get; set; }
    public ItemResource HolderItemResource { get; set; }

    public List<ItemResource> ItemResources
    {
        get => itemResources;
        set => itemResources = value;
    }

    protected override void DoEnable()
    {
        var data = npc.NPCData;
        if (data)
        {
            characterState = data.CharacterState;
            radiusCheckSellerResourceVariable = data.RadiusCheckResourceSeller;
            radiusCheckSupplierResourceVariable = data.RadiusCheckResourceSupplier;
            radiusCheckResourcePaymentVariable = data.RadiusCheckResourcePayment;
            timeDelayInteractSellerResourceVariable = data.TimeDelayCheckResourceSeller;
            timeDelayInteractSupplierResourceVariable = data.TimeDelayCheckResourceSupplier;
            timeDelayInteractResourcePaymentVariable = data.TimeDelayCheckResourcePayment;

            collectorItem = data.CollectorItemData;
            HolderItemResource = null;
            IsCompletePayment = false;
            collectorItem.ResetData();

            base.DoEnable();
            npc.OnComponentReady();
        }
    }

    protected override void OnDetectResourceSeller(ResourceSellerManager seller)
    {
        base.OnDetectResourceSeller(seller);

        if (npc.CurrentMissionTypeVariable.Value != ENPCMisstionType.BuyTomato) return;
        if (characterState.Value is ECharacterState.GoDestination or ECharacterState.Waiting) return;

        if (collectorItem.MaxCapacity() > itemResources.Count)
        {
            TryCollectItemFromSeller(seller);
        }

        if (IsCompleteCurrentMission())
        {
            CompleteMission();
        }
    }

    void TryCollectItemFromSeller(ResourceSellerManager seller)
    {
        var item = seller.GetResource();
        if (item && collectorItem.IsCanCollectItem(item.ItemType))
        {
            collectorItem.AddItemResource(new ItemCollectData(item.ItemType, item.Price));

            var data = collectorItem.ArrangeData;
            item.OnItemMove(resourceHolder, itemResources.Count, data.GetMaxNumberHorizontal, data.GetMaxNumberVertical,
                isArrangeHorizontal: data.IsArrangeHorizontal, isArrangeVertical: data.IsArrangeVertical,
                isArrange2D: data.IsArrange2D);

            itemResources.Add(item);
            npc.UpdateMissionUI();
            characterState.Value = ECharacterState.Carrying;
        }
    }

    protected override void OnDetectResourcePayment(ResourcePaymentManager payment)
    {
        base.OnDetectResourcePayment(payment);
        if (characterState.Value is ECharacterState.GoDestination or ECharacterState.Waiting) return;

        if (npc.CurrentMissionTypeVariable.Value == ENPCMisstionType.PaymentItemResource && !IsCompletePayment)
        {
            payment.OnReadyDoPayment(this);
        }
    }

    bool IsCompleteCurrentMission()
    {
        return collectorItem.GetCurrentItemMissionAmount(npc.CurrentMissionSetup.ItemMissionType)
               >= npc.CurrentMissionSetup.GetTargetAmount;
    }

    void CompleteMission()
    {
        npc.CompleteCurrentMission();
    }

    public void PaymentValue(int totalValue, Action completePayment, Transform holderMoney, int priceUnit,
        ArrangeData arrangeData, int currentMoneyCount)
    {
        int amountToPay = totalValue / priceUnit;
        int paid = 0;

        for (int i = currentMoneyCount - amountToPay; i < currentMoneyCount; i++)
        {
            MoneySpawnerController.Instance.OnSpawnItemMove(
                HolderItem, holderMoney, i,
                arrangeData.GetMaxNumberHorizontal, arrangeData.GetMaxNumberVertical,
                arrangeData.IsArrangeVertical, arrangeData.IsArrangeHorizontal,
                CheckCompleteMoneyPayment,
                isArrange2D: arrangeData.IsArrange2D
            );
        }

        void CheckCompleteMoneyPayment()
        {
            if (++paid >= amountToPay)
                completePayment?.Invoke();
        }
    }

    public void OnCompletePayment()
    {
        var data = collectorItem.ArrangeData;

        HolderItemResource.OnItemMove(resourceHolder, 0, data.GetMaxNumberHorizontal, data.GetMaxNumberVertical,
            data.IsArrangeVertical, data.IsArrangeHorizontal, CompleteMission, data.IsArrange2D);

        itemResources.Add(HolderItemResource);
        collectorItem.AddItemResource(new ItemCollectData(HolderItemResource.ItemType, HolderItemResource.Price));
    }
}