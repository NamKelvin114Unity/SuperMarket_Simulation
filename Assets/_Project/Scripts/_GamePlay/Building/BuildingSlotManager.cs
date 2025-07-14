using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VirtueSky.Inspector;

public class BuildingSlotManager : MonoBehaviour
{
    [SerializeField] private bool isSlotMultiple;
    [SerializeField] private List<BuildingSlotData> buildingSlotData;

    public bool GetAvailableSlot(out Transform slot, out bool isWaitingSlot, NPC getNPC)
    {
        slot = null;
        isWaitingSlot = false;
        BuildingSlotData availableSlot = null;
        availableSlot = buildingSlotData.FirstOrDefault(b => b.IsSlotAvailable);
        if (availableSlot != null)
        {
            slot = availableSlot.Slot;
            if (!isSlotMultiple)
            {
                availableSlot.CurrentNPC = getNPC;
            }

            isWaitingSlot = availableSlot.IsWaitingSlot;
            return true;
        }

        return false;
    }

    public bool IsFullSlot()
    {
        return buildingSlotData.FirstOrDefault(b => b.IsSlotAvailable) == null;
    }

    public void ReSetSlot(NPC getNPC)
    {
        var availableSlot = buildingSlotData.FirstOrDefault(b => b.CurrentNPC == getNPC);
        if (availableSlot != null)
            availableSlot.CurrentNPC = null;
    }
}

[Serializable]
public class BuildingSlotData
{
    [SerializeField] private Transform slot;
    [SerializeField] private bool isWaitingSlot;
    [ReadOnly] [SerializeField] private NPC currentNPC;
    public Transform Slot => slot;
    public bool IsWaitingSlot => isWaitingSlot;

    public NPC CurrentNPC
    {
        get => currentNPC;
        set => currentNPC = value;
    }

    public bool IsSlotAvailable => !currentNPC;
}