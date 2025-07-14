using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using VirtueSky.Core;
using VirtueSky.Inspector;
using VirtueSky.Linq;
using VirtueSky.ObjectPooling;

public class ResourceSupplierManager : MonoBehaviour, IResource
{
    [FormerlySerializedAs("resourceData")] [HeaderLine("Data")] [SerializeField]
    private ResourceSupplierData resourceSupplierData;

    [HeaderLine("Properties")] [SerializeField]
    List<ResourceHolder> resourceHolders = new List<ResourceHolder>();

    [ReadOnly] [SerializeField] List<ItemResource> itemResources = new List<ItemResource>();

    private bool _isFullResource;
    private ResourceSupplierData _currentResourceSupplier;

    public bool IsCanAddResource(EItemType itemType)
    {
        return true;
    }

    public Transform ResourceHolder
    {
        get => resourceHolders[0].holder;
    }

    private void Start()
    {
        _currentResourceSupplier = resourceSupplierData;
        itemResources.Clear();
        _isFullResource = false;
        OnUpdateStatusResource();
    }

    void OnUpdateStatusResource()
    {
        App.Delay(this, resourceSupplierData.GetTimeSpawnResourceValue, (() =>
        {
            if (itemResources.Count < _currentResourceSupplier.GetMaxValueResource)
            {
                OnSpawnResource();
            }
            else
            {
                _isFullResource = true;
            }
        }));
    }


    void OnSpawnResource()
    {
        var getHolder = resourceHolders.FirstOrDefault(c => !c.isNotEmpty);
        var obj = resourceSupplierData.GetItemResourceValue.gameObject.SpawnCustom(parent: getHolder.holder).gameObject;
        obj.transform.localPosition = Vector3.zero;
        getHolder.itemResource = obj;
        getHolder.isNotEmpty = true;
        var item = obj.GetComponentInParent<ItemResource>();
        if (item != null)
        {
            AddResource(item);
        }

        OnUpdateStatusResource();
    }

    public void AddResource(ItemResource resource)
    {
        itemResources.Add(resource);
        resourceSupplierData.AddItem(new ItemCollectData(resource.ItemType, resource.Price));
    }

    public ItemResource GetResource()
    {
        ItemResource getItem = null;
        if (itemResources.Count <= 0) return getItem;
        getItem = itemResources.FirstOrDefault(i => i.IsCollectable);
        if (getItem)
        {
            itemResources.Remove(getItem);
            resourceSupplierData.RemoveItem(new ItemCollectData(getItem.ItemType, getItem.Price));
            var getHolder = resourceHolders.FirstOrDefault(r => r.itemResource == getItem.gameObject);
            if (getHolder != null)
            {
                getHolder.isNotEmpty = false;
                getHolder.itemResource = null;
            }

            if (_isFullResource)
            {
                _isFullResource = false;
                OnUpdateStatusResource();
            }
        }

        return getItem;
    }
}

[Serializable]
public class ResourceHolder
{
    public Transform holder;
    [ReadOnly] public bool isNotEmpty;
    [ReadOnly] public GameObject itemResource;
}