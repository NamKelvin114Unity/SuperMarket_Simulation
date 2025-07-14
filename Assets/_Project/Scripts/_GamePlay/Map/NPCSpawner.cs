using System;
using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Events;
using VirtueSky.Inspector;
using VirtueSky.ObjectPooling;
using VirtueSky.Variables;
using Random = UnityEngine.Random;

public class NPCSpawner : MonoBehaviour
{
    [HeaderLine("Data")] [SerializeField] IntegerVariable maxSpawnAmountVariable;
    [SerializeField] FloatVariable timeDelaySpawnVariable;
    [HeaderLine("Event")] [SerializeField] private EventNoParam onSpawnNPCEvent;
    [SerializeField] private EventNoParam onDeSpawnNPCEvent;

    [HeaderLine("Properties")] [SerializeField]
    private NPC npc;

    [SerializeField] List<NPCData> npcDatas = new List<NPCData>();

    [ReadOnly] [SerializeField] List<NPC> currentNPCs = new List<NPC>();
    [ReadOnly] [SerializeField] List<NPCData> currentNPCData = new List<NPCData>();

    [SerializeField] private Transform holderSpawn;
    private bool _isSpawning = false;

    private void OnEnable()
    {
        onSpawnNPCEvent.OnRaised += OnRespawn;
        onDeSpawnNPCEvent.OnRaised += OnDespawn;
    }

    private void OnDisable()
    {
        onSpawnNPCEvent.OnRaised -= OnRespawn;
        onDeSpawnNPCEvent.OnRaised -= OnDespawn;
    }

    private void Start()
    {
        foreach (var data in npcDatas)
        {
            currentNPCData.Add(data);
        }

        OnPreSpawn();
    }

    void OnPreSpawn()
    {
        if (currentNPCs.Count < maxSpawnAmountVariable.Value)
        {
            OnSpawnNPC(npc);
            App.Delay(this, timeDelaySpawnVariable.Value, (OnPreSpawn));
        }
    }

    void OnRespawn()
    {
        for (int i = 0; i < currentNPCs.Count; i++)
        {
            OnSpawnNPC(npc);
        }
    }

    void OnDespawn()
    {
        for (int i = 0; i < currentNPCs.Count; i++)
        {
            if (!currentNPCs[i].gameObject.activeInHierarchy)
            {
                if (!currentNPCData.Contains(currentNPCs[i].NPCData))
                {
                    currentNPCData.Add(currentNPCs[i].NPCData);
                }
            }
        }

        App.Delay(this, timeDelaySpawnVariable.Value, (() => { OnSpawnNPC(npc); }));
    }

    void OnSpawnNPC(NPC getNpc)
    {
        var getRandomNpcData = currentNPCData[Random.Range(0, currentNPCData.Count)];
        if (currentNPCData.Contains(getRandomNpcData))
        {
            currentNPCData.Remove(getRandomNpcData);
        }

        var spawnNpc = getNpc.SpawnCustom(holderSpawn,
            initAction: (o => { o.GetComponent<NPC>().Init(getRandomNpcData); }));
        spawnNpc.transform.localPosition = Vector3.zero;
        if (!currentNPCs.Contains(spawnNpc))
        {
            currentNPCs.Add(spawnNpc);
        }
    }
}