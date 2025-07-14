using System.Collections.Generic;
using UnityEngine;
using VirtueSky.Core;
using VirtueSky.Events;
using VirtueSky.Inspector;
using VirtueSky.ObjectPooling;

public class DeSpawnNPC : MonoBehaviour
{
    [HeaderLine("Event")] [SerializeField] private EventNoParam onDeSpawnEvent;
    [SerializeField] EventNoParam onReSpawnEvent;
    [ReadOnly] [SerializeField] List<NPC> currentDeSpawnNPCs = new List<NPC>();


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constant.NPC_TAG))
        {
            var npc = other.gameObject.GetComponent<NPC>();
            if (npc.CurrentMissionTypeVariable.Value == ENPCMisstionType.GoHome && !currentDeSpawnNPCs.Contains(npc))
            {
                currentDeSpawnNPCs.Add(npc);
                OnDeSpawn(npc);
            }
        }
    }

    void OnDeSpawn(NPC npc)
    {
        npc.gameObject.DeSpawnCustom();
        onDeSpawnEvent.Raise();
        currentDeSpawnNPCs.Remove(npc);
        // onReSpawnEvent.Raise();
    }
}