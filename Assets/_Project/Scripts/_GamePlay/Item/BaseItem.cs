using System;
using PrimeTween;
using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public abstract class BaseItem : MonoBehaviour, IItem
{
    [HeaderLine("Core")] [SerializeField] protected bool isCollectable;
    [SerializeField] protected FloatVariable itemMoveTimeVariable;
    [SerializeField] protected EItemType itemType;

    public bool IsCollectable
    {
        get => isCollectable;
        set => isCollectable = value;
    }

    public void OnItemMove(Vector3 destination, Action onMoveComplete = null)
    {
        transform.DOMove(destination, itemMoveTimeVariable.Value).OnComplete((() => { onMoveComplete?.Invoke(); }), warnIfTargetDestroyed: false);
    }
}

public enum EItemType
{
    None,
    Tomato,
}