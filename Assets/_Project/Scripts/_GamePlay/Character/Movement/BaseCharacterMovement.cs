using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public abstract class BaseCharacterMovement : MonoBehaviour
{
    [HeaderLine("Base")] [SerializeField] protected Rigidbody rigidbody;
    [SerializeField] protected FloatVariable characterMoveSpeedVariable;
    [SerializeField] protected FloatVariable minDistanceMovementVariable;
    [SerializeField] protected FloatVariable characterRotateSpeedVariable;
    [SerializeField] protected CharacterStateVariable characterStateVariable;
    [SerializeField] protected ECharacterMovement characterMovement;
}