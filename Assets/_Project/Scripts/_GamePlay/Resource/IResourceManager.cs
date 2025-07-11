using System.Collections.Generic;

public interface IResourceManager
{
    HashSet<BaseItem> ResourceList { get; set; }
}