using UnityEngine;

public interface INPCMovement
{
    void MoveToDestination(Vector3 destination, bool isWaitingSlot = false);
}