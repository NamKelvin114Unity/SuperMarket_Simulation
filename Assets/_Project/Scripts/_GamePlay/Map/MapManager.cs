using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using VirtueSky.Inspector;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour
{
    [HeaderLine("Events")] [SerializeField]
    NPCCheckMissionInforEvent checkMissionInforEvent;

    [SerializeField] NPCGetMissionInforEvent getMissionInforEvent;

    [HeaderLine("Properties")] [SerializeField]
    private List<MissionSetup> missionSetups = new List<MissionSetup>();

    [FormerlySerializedAs("npcWaitingMissions")] [ReadOnly] [SerializeField]
    List<NPCCheckMissionInfor> npcWaitingMissionsList = new List<NPCCheckMissionInfor>();

    Stack<NPCCheckMissionInfor> npcWaitingMissionsStack = new Stack<NPCCheckMissionInfor>();
    Queue<NPCCheckMissionInfor> npcWaitingMissionsQueue = new Queue<NPCCheckMissionInfor>();

    private void OnEnable()
    {
        checkMissionInforEvent.OnRaised += OnCheckMissionNPC;
    }

    private void OnDisable()
    {
        checkMissionInforEvent.OnRaised -= OnCheckMissionNPC;
    }

    private void Update()
    {
    }

    void OnShowMissionNPC()
    {
        while (npcWaitingMissionsStack.Count > 0)
        {
            var mission = npcWaitingMissionsStack.Pop();
            if (mission != null && mission.BuildingSlotManager)
            {
                mission.BuildingSlotManager.ReSetSlot(mission.NPC);
            }

            var getMissionSetup =
                missionSetups.FirstOrDefault(m => m.GetMissionType == mission.NpcMissionSetup.GetMissionType);
            if (getMissionSetup == null)
            {
                RaiseMission(mission, null, null);
            }
            else
            {
                var buildingSlot = getMissionSetup.GetBuildingSlotManager();
                if (!buildingSlot)
                {
                    RaiseMission(mission, null, null);
                }
                else
                {
                    if (buildingSlot.GetAvailableSlot(out Transform destination, out bool isWaitingSlot, mission.NPC))
                    {
                        RaiseMission(mission, destination, buildingSlot, isWaitingSlot);
                    }
                    else
                    {
                        RaiseMission(mission, null, null);
                    }
                }
            }
        }

        void RaiseMission(NPCCheckMissionInfor npcMission, Transform destination,
            BuildingSlotManager buildingSlotManager, bool isWaitingSlot = false)
        {
            getMissionInforEvent.Raise(new NPCGetMissionInfor(npcMission.NPC, destination, buildingSlotManager,
                isWaitingSlot));
            if (!isWaitingSlot && destination)
            {
                npcWaitingMissionsList.Remove(npcMission);
                if (npcWaitingMissionsQueue.Count > 0)
                {
                    var waitingNpcMission = npcWaitingMissionsQueue.Dequeue();
                    npcWaitingMissionsStack.Push(waitingNpcMission);
                }
            }
            else
            {
                if (buildingSlotManager)
                {
                    npcMission.BuildingSlotManager = buildingSlotManager;
                }

                npcWaitingMissionsQueue.Enqueue(npcMission);
            }
        }
    }

    void OnCheckMissionNPC(NPCCheckMissionInfor npcCheckMissionInfor)
    {
        npcWaitingMissionsStack.Push(npcCheckMissionInfor);
        if (!npcWaitingMissionsList.Contains(npcCheckMissionInfor))
        {
            npcWaitingMissionsList.Add(npcCheckMissionInfor);
        }

        OnShowMissionNPC();
    }
}

[Serializable]
public class MissionSetup
{
    [SerializeField] ENPCMisstionType missionType;
    [SerializeField] List<BuildingSlotManager> buildingSlotManager = new List<BuildingSlotManager>();

    public BuildingSlotManager GetBuildingSlotManager()
    {
        var listAvailable = buildingSlotManager.Where(b => !b.IsFullSlot() && b.gameObject.activeInHierarchy).ToList();
        if (listAvailable.Count <= 0) return null;
        return listAvailable[Random.Range(0, listAvailable.Count)];
    }

    public ENPCMisstionType GetMissionType => missionType;
}