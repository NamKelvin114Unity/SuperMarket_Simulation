using System;
using UnityEngine;
using UnityEngine.AI;
using VirtueSky.Core;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public abstract class NPC : MonoBehaviour
{
    [HeaderLine("Core")] [SerializeField] protected FloatVariable timeDelayInitVariable;
    [SerializeField] protected NPCData npcData;
    [SerializeField] protected int componentAmount;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] protected NPCUIController uiController;
    [HeaderLine("Data")] [SerializeField] protected NPCMissionData missionData;
    [SerializeField] protected CharacterStateVariable characterStateVariable;
    [SerializeField] protected NPCMissionTypeVariable currentMissionTypeVariable;
    [SerializeField] protected CollectorItemData collectorItemData;
    [HeaderLine("Event")] [SerializeField] protected NPCCheckMissionInforEvent checkMissionInforEvent;
    [SerializeField] protected NPCGetMissionInforEvent getMissionInforEvent;
    [ReadOnly] [SerializeField] protected Transform currentDestination;
    protected int currentMissionIndex = 0;
    NPCGetMissionInfor _currentGetMissionInfor = null;
    protected int currentAmountComponent = 0;
    protected bool isReady = false;
    protected NPCMissionSetup currentMissionSetup;

    public NPCData NPCData => npcData;
    public NPCMissionTypeVariable CurrentMissionTypeVariable => currentMissionTypeVariable;

    public NPCMissionSetup CurrentMissionSetup => currentMissionSetup;


    public void Init(NPCData getnpcData)
    {
        navMeshAgent.enabled = false;
        rigidbody.isKinematic = true;
        isReady = false;
        currentDestination = null;
        currentMissionIndex = 0;
        currentAmountComponent = 0;
        npcData = getnpcData;
        missionData = npcData.MissionData;
        characterStateVariable = npcData.CharacterState;
        currentMissionTypeVariable = npcData.MissionType;
        currentMissionTypeVariable.Value = ENPCMisstionType.None;
        collectorItemData = npcData.CollectorItemData;
        Material[] mats = { npcData.Material };
        skinnedMeshRenderer.materials = mats;
    }

    public void OnComponentReady()
    {
        currentAmountComponent++;
    }

    private void Update()
    {
        if (currentAmountComponent >= componentAmount && !isReady)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
            navMeshAgent.avoidancePriority = 0;
            isReady = true;
            rigidbody.isKinematic = false;
            App.Delay(this, timeDelayInitVariable.Value, (OnUpdateMission));
        }
    }

    private void OnEnable()
    {
        getMissionInforEvent.OnRaised += OnGetMissionInfor;
    }

    private void OnDisable()
    {
        getMissionInforEvent.OnRaised -= OnGetMissionInfor;
    }

    protected virtual void OnGetMissionInfor(NPCGetMissionInfor npcGetMissionInfor)
    {
        if (npcGetMissionInfor.NPC == this && npcGetMissionInfor.MissionDestination)
        {
            _currentGetMissionInfor = npcGetMissionInfor;
            currentDestination = _currentGetMissionInfor.MissionDestination;
            OnStartMission(_currentGetMissionInfor);
        }
    }

    protected abstract void OnStartMission(NPCGetMissionInfor npcGetMissionInfor);


    public void CompleteCurrentMission()
    {
        currentMissionIndex++;
        OnUpdateMission();
    }

    public void UpdateMissionUI()
    {
        var currentValue = collectorItemData.GetCurrentItemMissionAmount(currentMissionSetup.ItemMissionType);
        var maxValue = currentMissionSetup.GetTargetAmount;
        uiController.OnShowUIMission(currentMissionSetup.IsShowText, currentMissionSetup.GetIconMission,
            currentMissionSetup.FormatDescription(currentValue, maxValue),
            timeDeActive: currentMissionSetup.TimeDeActive);
    }

    protected void OnUpdateMission()
    {
        if (!missionData.IsComplete(currentMissionIndex))
        {
            currentMissionSetup = missionData.GetMission(currentMissionIndex);
            currentMissionTypeVariable.Value = currentMissionSetup.GetMissionType;
            BuildingSlotManager currenBuildingSlot = null;
            if (_currentGetMissionInfor != null && _currentGetMissionInfor.BuidlingSlotManager)
            {
                currenBuildingSlot = _currentGetMissionInfor.BuidlingSlotManager;
            }

            checkMissionInforEvent.Raise(new NPCCheckMissionInfor(this, currentMissionSetup, currenBuildingSlot));
            UpdateMissionUI();
        }
    }
}

public class NPCGetMissionInfor
{
    NPC npc;
    Transform missionDestination;
    bool isWaitingSlot = false;
    BuildingSlotManager buildingSlotManager;


    public NPCGetMissionInfor(NPC npc, Transform missionDestination, BuildingSlotManager buildingSlotManager,
        bool isWaitingSlot)
    {
        this.isWaitingSlot = isWaitingSlot;
        this.buildingSlotManager = buildingSlotManager;
        this.npc = npc;
        this.missionDestination = missionDestination;
    }

    public bool IsWaitingSlot => isWaitingSlot;

    public NPC NPC
    {
        get => npc;
        set => npc = value;
    }

    public Transform MissionDestination
    {
        get => missionDestination;
        set => missionDestination = value;
    }

    public BuildingSlotManager BuidlingSlotManager
    {
        get => buildingSlotManager;
        set => buildingSlotManager = value;
    }
}

[Serializable]
public class NPCCheckMissionInfor
{
    [SerializeField] NPC npc;
    [SerializeField] NPCMissionSetup npcNpcMissionSetup;
    [SerializeField] BuildingSlotManager buildingSlotManager;

    public NPCCheckMissionInfor(NPC npc, NPCMissionSetup npcNpcMissionSetup, BuildingSlotManager buildingSlotManager)
    {
        this.npc = npc;
        this.npcNpcMissionSetup = npcNpcMissionSetup;
        this.buildingSlotManager = buildingSlotManager;
    }

    public NPC NPC
    {
        get => npc;
        set => npc = value;
    }

    public NPCMissionSetup NpcMissionSetup
    {
        get => npcNpcMissionSetup;
        set => npcNpcMissionSetup = value;
    }

    public BuildingSlotManager BuildingSlotManager
    {
        get => buildingSlotManager;
        set => buildingSlotManager = value;
    }
}