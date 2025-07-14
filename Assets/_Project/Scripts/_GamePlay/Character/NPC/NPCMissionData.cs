using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName = "Data/NPCMissionData", fileName = "npc_mission_data")]
public class NPCMissionData : ScriptableObject
{
    [SerializeField] List<NPCMissionSetup> npcMissionData = new List<NPCMissionSetup>();

    public NPCMissionSetup GetMission(int currentMissionIndex)
    {
        var missionSetup = npcMissionData[currentMissionIndex];
        missionSetup.InitTargetAmount();
        return missionSetup;
    }

    public bool IsComplete(int currentMissionIndex) => currentMissionIndex >= npcMissionData.Count;
}

[Serializable]
public class NPCMissionSetup
{
    [SerializeField] ENPCMisstionType missionType;
    [SerializeField] List<EItemType> itemMissionType = new List<EItemType>();
    [SerializeField] private List<Sprite> iconMission = new List<Sprite>();
    [SerializeField] private string formatDescription;
    [SerializeField] private bool isShowText;
    [SerializeField] private int targetAmount;
    [SerializeField] private bool isRandomValue;
    [SerializeField] private int timeDeActive;
    private int _currentTargetAmount;
    public ENPCMisstionType GetMissionType => missionType;
    public Sprite GetIconMission => iconMission[Random.Range(0, iconMission.Count)];

    public string FormatDescription(int currentValue, int maxValue)
    {
        return string.Format(formatDescription, currentValue, maxValue);
    }

    public int TimeDeActive => timeDeActive;

    public List<EItemType> ItemMissionType => itemMissionType;

    public void InitTargetAmount()
    {
        if (isRandomValue) _currentTargetAmount = Random.Range(1, targetAmount);
        else _currentTargetAmount = targetAmount;
    }

    public int GetTargetAmount => _currentTargetAmount;

    public bool IsCorrectItemMissionType(EItemType itemType) => itemMissionType.Contains(itemType);

    public bool IsShowText => isShowText;
}

public enum ENPCMisstionType
{
    BuyTomato,
    PaymentItemResource,
    None,
    GoHome,
}