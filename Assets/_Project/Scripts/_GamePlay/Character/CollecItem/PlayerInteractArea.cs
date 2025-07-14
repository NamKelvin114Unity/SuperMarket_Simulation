using UnityEngine;

public class PlayerInteractArea : CharacterInteractArea
{
    [SerializeField] PlayerUIController playerUIController;

    protected override void OnDetectResourceSeller(ResourceSellerManager resourceSellerManager)
    {
        base.OnDetectResourceSeller(resourceSellerManager);
        if (itemResources.Count > 0)
        {
            for (int i = itemResources.Count - 1; i >= 0; i--)
            {
                var getItem = itemResources[i];
                if (resourceSellerManager.IsCanAddResource(getItem.ItemType))
                {
                    playerUIController.SetGroupMaxItem(false);
                    itemResources.Remove(getItem);
                    collectorItem.RemoveItemResource(new ItemCollectData(getItem.ItemType, getItem.Price));
                    getItem.OnItemMove(resourceSellerManager.ResourceHolder, resourceSellerManager.ResourceCount,
                        resourceSellerManager.GetMaxCapacityHorizontal, resourceSellerManager.GetMaxCapacityVertical
                        , resourceSellerManager.ArrangeVertical, resourceSellerManager.ArrangeHorizontal, null,
                        resourceSellerManager.Arrange2D);
                    resourceSellerManager.AddResource(getItem);
                }
            }
        }
        else
        {
            characterState.Value = ECharacterState.NonCarry;
        }
    }

    protected override void OnDetectResourcePayment(ResourcePaymentManager resourceManagerPayment)
    {
        base.OnDetectResourcePayment(resourceManagerPayment);
        resourceManagerPayment.OnStartDoPayment(resourceHolder);
    }

    protected override void OnDetectResourceSupplier(ResourceSupplierManager resourceSupplierManager)
    {
        base.OnDetectResourceSupplier(resourceSupplierManager);
        if (collectorItem.MaxCapacity() > itemResources.Count)
        {
            var item = resourceSupplierManager.GetResource();
            if (item && collectorItem.IsCanCollectItem(item.ItemType))
            {
                collectorItem.AddItemResource(new ItemCollectData(item.ItemType, item.Price));
                var dataArrange = collectorItem.ArrangeData;
                item.OnItemMove(resourceHolder, itemResources.Count, dataArrange.GetMaxNumberHorizontal,
                    dataArrange.GetMaxNumberVertical,
                    isArrangeHorizontal: dataArrange.IsArrangeHorizontal,
                    isArrangeVertical: dataArrange.IsArrangeVertical, isArrange2D: dataArrange.IsArrange2D,
                    isPlayAnimDoneMove: true);
                itemResources.Add(item);
                characterState.Value = ECharacterState.Carrying;
            }
        }
        else
        {
            playerUIController.SetGroupMaxItem(true);
        }
    }
}