using System;
using UnityEngine;
using UnityEngine.AI;
using VirtueSky.Inspector;

public class NPCCustomerMovement : BaseCharacterMovement, INPCMovement
{
    [HeaderLine("Core")] [SerializeField] private NPC npc;
    [SerializeField] private Transform model;

    [HeaderLine("Properties")] [SerializeField]
    protected NavMeshAgent navMeshAgent;

    [SerializeField] NPCCustomerAnimationController npcCustomerAnimationController;
    public float GetMoveSpeed => characterMoveSpeedVariable.Value;
    public float GetRotateSpeed => characterRotateSpeedVariable.Value;
    public bool _isWaitingSlot;

    private void OnEnable()
    {
        if (npc && npc.NPCData)
        {
            characterStateVariable = npc.NPCData.CharacterState;
            characterMoveSpeedVariable = npc.NPCData.MoveSpeed;
            characterRotateSpeedVariable = npc.NPCData.RotationSpeed;
            minDistanceMovementVariable = npc.NPCData.MoveMinDistace;
            npc.OnComponentReady();
        }
    }

    public void MoveToDestination(Vector3 destination, bool isWaitingSlot = false)
    {
        _isWaitingSlot = isWaitingSlot;
        characterStateVariable.Value = ECharacterState.GoDestination;
        if (navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.SetDestination(destination);
            navMeshAgent.updateRotation = false;
        }
    }

    bool IsCarrying() => npc.NPCData.CollectorItemData.GetCurrentItemAmount > 0;

    private void Update()
    {
        if (navMeshAgent.hasPath && navMeshAgent.remainingDistance > minDistanceMovementVariable.Value)
        {
            if (IsCarrying())
                npcCustomerAnimationController.OnPlayRunCarryAnim();
            else npcCustomerAnimationController.OnPlayRunAnim();
            if (navMeshAgent.velocity.normalized != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
                model.rotation = Quaternion.Slerp(model.rotation, targetRotation, GetRotateSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (_isWaitingSlot) characterStateVariable.Value = ECharacterState.Waiting;
            else characterStateVariable.Value = ECharacterState.Idle;
            if (IsCarrying())
                npcCustomerAnimationController.OnPlayIdleCarryAnim();
            else npcCustomerAnimationController.OnPlayIdleAnim();
        }
    }
}