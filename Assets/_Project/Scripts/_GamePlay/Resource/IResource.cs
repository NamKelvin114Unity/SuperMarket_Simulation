using TheBeginning.Config;
using UnityEngine;

public interface IResource
{
    bool IsCanAddResource(EItemType itemType);
    Transform ResourceHolder { get; }
    ItemResource GetResource();
    void AddResource(ItemResource resource);
}