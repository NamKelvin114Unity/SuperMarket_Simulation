using System;
using UnityEngine;

public interface IItem
{
    bool IsCollectable { get; set; }
    void OnItemMove(Vector3 destination, Action onMoveComplete = null);
}