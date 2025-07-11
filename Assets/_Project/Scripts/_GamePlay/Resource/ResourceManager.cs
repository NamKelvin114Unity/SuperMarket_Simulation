using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Inspector;
using VirtueSky.Linq;
using VirtueSky.ObjectPooling;
using VirtueSky.Variables;

public class ResourceManager : MonoBehaviour, IResourceManager
{
    [HeaderLine("Data")] [SerializeField] private ResourceData resourceData;

    [HeaderLine("Properties")] [SerializeField]
    private FloatVariable timeSpawnResourceVariable;

    [SerializeField] List<ResourceHolder> resourceHolders = new List<ResourceHolder>();

    [HeaderLine("Tracking")] [ReadOnly] [SerializeField]
    private List<BaseItem> currentResourceList = new List<BaseItem>();

    private HashSet<BaseItem> resourceList = new HashSet<BaseItem>();
    private bool _isFullResource;

    public HashSet<BaseItem> ResourceList
    {
        get => resourceList;
        set => resourceList = value;
    }

    private void Start()
    {
        _isFullResource = false;
    }

    void OnUpdateStatusResource()
    {
        App.Delay(this, resourceData.GetTimeSpawnResourceValue, (() =>
        {
            if (ResourceList.Count < resourceData.GetMaxValueResource)
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
        var obj = resourceData.GetItemResourceValue.gameObject.Spawn(parent: getHolder.holder);
        getHolder.itemResource = obj;
        getHolder.isNotEmpty = true;
        var item = obj.GetComponent<BaseItem>();
        if (item != null)
        {
            resourceList.Add(item);
            currentResourceList.Add(item);
        }

        OnUpdateStatusResource();
    }

    BaseItem GetResource()
    {
        BaseItem getItem = null;
        if (ResourceList.Count <= 0) return getItem;
        getItem = ResourceList.First();
        ResourceList.Remove(getItem);
        var getHolder = resourceHolders.FirstOrDefault(r => r.itemResource == getItem);
        getHolder.isNotEmpty = false;
        getHolder.itemResource = null;
        if (_isFullResource)
        {
            _isFullResource = false;
            OnUpdateStatusResource();
        }

        return getItem;
    }
}

[Serializable]
public class ResourceHolder
{
    public Transform holder;
    public bool isNotEmpty;
    public GameObject itemResource;
}