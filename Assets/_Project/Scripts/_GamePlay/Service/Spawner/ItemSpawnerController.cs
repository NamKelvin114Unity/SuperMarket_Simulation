using System;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;
using UnityEngine.Serialization;
using VirtueSky.ObjectPooling;

public abstract class ItemSpawnerController : ComponentSingleton<ItemSpawnerController>
{
    [SerializeField] private ItemResource itemResource;

    [FormerlySerializedAs("transform")] [SerializeField]
    private Transform holder;

    public void OnSpawnItemMove(Transform startPos, Transform destination, int numberIndexItem,
        int maxNumberHorizontal, int maxNumberVertical,
        bool isArrangeVertical = false,
        bool isArrangeHorizontal = false, Action onMoveComplete = null, bool isArrange2D = true,
        bool isDeActiveWhenMoveDone = false)
    {
        var item = itemResource.gameObject.SpawnCustom(holder).gameObject;
        item.transform.position = startPos.position;
        var moveItem = item.GetComponent<ItemResource>();
        moveItem.OnItemMove(destination, numberIndexItem, maxNumberHorizontal, maxNumberVertical, isArrangeVertical,
            isArrangeHorizontal, (() =>
            {
                onMoveComplete?.Invoke();
                if (isDeActiveWhenMoveDone)
                {
                    moveItem.DeSpawnCustom();
                }
            }), isArrange2D);
    }
}