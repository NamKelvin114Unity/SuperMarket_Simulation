using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using VirtueSky.Component;
using VirtueSky.Inspector;
using VirtueSky.ObjectPooling;
using VirtueSky.Variables;

public abstract class ItemResource : MonoBehaviour
{
    [HeaderLine("Core")] [SerializeField] private Vector3Variable itemSize;
    [SerializeField] List<ItemResource> itemResources = new List<ItemResource>();
    [SerializeField] protected bool isCollectable;
    [SerializeField] private IntegerVariable priceVariable;
    [SerializeField] protected FloatVariable itemMoveTimeVariable;
    [SerializeField] protected EItemType itemType;
    [SerializeField] protected bool isUseAnimationMoveComplete;
    [SerializeField] protected HandleAnimancerComponent handleAnimancerComponent;

    [ShowIf(nameof(isUseAnimationMoveComplete))] [SerializeField]
    protected AnimationClip moveCompleteAnimationClip;


    public bool IsCollectable
    {
        get => isCollectable;
        set => isCollectable = value;
    }

    public EItemType ItemType => itemType;

    public Vector3Variable ItemSize => itemSize;

    public int Price => priceVariable.Value;

    public void AddItemResource(ItemResource itemResource)
    {
        itemResources.Add(itemResource);
    }

    public void DeSpawnItem()
    {
        if (itemResources.Count > 0)
        {
            foreach (var item in itemResources)
            {
                item.DeSpawnItem();
            }
        }

        if (gameObject.GetComponent<PooledObjectIdCustom>())
        {
            gameObject.DeSpawnCustom();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    protected virtual void OnMoveComplete(bool isPlayAnimDoneMove)
    {
        if (isPlayAnimDoneMove && isUseAnimationMoveComplete)
        {
            handleAnimancerComponent.PlayAnim(moveCompleteAnimationClip, _durationFade: 0);
        }
    }

    public void OnItemMove(Transform destination, int numberIndexItem, int maxNumberHorizontal, int maxNumberVertical,
        bool isArrangeVertical = false,
        bool isArrangeHorizontal = false,
        Action onMoveComplete = null,
        bool isArrange2D = false, bool isPlayAnimDoneMove = false)
    {
        if (maxNumberHorizontal <= 0 || maxNumberVertical <= 0)
        {
            Debug.LogError("maxNumberHorizontal and maxNumberVertical must be > 0");
            return;
        }

        transform.DOMove(destination.position, itemMoveTimeVariable.Value)
            .OnComplete(() =>
            {
                transform.parent = destination;

                int layerSize = maxNumberHorizontal * maxNumberVertical;
                int layer = numberIndexItem / layerSize;
                int indexInLayer = numberIndexItem % layerSize;

                int xIndex = 0;
                int yIndex = 0;
                int zIndex = 0;

                if (isArrange2D)
                {
                    if (isArrangeHorizontal)
                        xIndex = indexInLayer % maxNumberHorizontal;

                    if (isArrangeVertical)
                        yIndex = indexInLayer / maxNumberHorizontal;

                    if (isArrangeHorizontal && isArrangeVertical)
                        zIndex = layer;
                }
                else
                {
                    if (isArrangeHorizontal)
                        xIndex = indexInLayer % maxNumberHorizontal;

                    if (isArrangeVertical)
                        yIndex = layer;

                    if (isArrangeVertical)
                        zIndex = indexInLayer / maxNumberHorizontal;
                }

                Vector3 offset = new Vector3(
                    itemSize.Value.x * xIndex,
                    itemSize.Value.y * yIndex,
                    itemSize.Value.z * zIndex
                );

                transform.localPosition = offset;
                OnMoveComplete(isPlayAnimDoneMove);
                onMoveComplete?.Invoke();
            }, warnIfTargetDestroyed: false);
    }
}

public enum EItemType
{
    None,
    Tomato,
    Money,
    Box,
}