using UnityEngine;
using UnityEngine.Serialization;

public class NPCCustomer : NPC
{
    [FormerlySerializedAs("nPCMovement")] [SerializeField]
    NPCCustomerMovement nPCCustomerMovement;

    protected override void OnStartMission(NPCGetMissionInfor npcGetMissionInfor)
    {
        if (npcGetMissionInfor.IsWaitingSlot) characterStateVariable.Value = ECharacterState.Waiting;
        nPCCustomerMovement.MoveToDestination(npcGetMissionInfor.MissionDestination.position,
            npcGetMissionInfor.IsWaitingSlot);
    }
}