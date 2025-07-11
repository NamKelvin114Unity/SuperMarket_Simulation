using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public class PlayerMovement : BaseCharacterMovement, IJoystickMovement
{
    [HeaderLine("Variable")] [SerializeField]
    private Vector3Variable joystickVariable;

    [SerializeField] private Vector3Variable ownerPositionVariable;

    public Vector3Variable JoystickVariable
    {
        get => joystickVariable;
        set => joystickVariable = value;
    }

    public Vector3Variable OwnerPositionVariable
    {
        get => ownerPositionVariable;
        set => ownerPositionVariable = value;
    }

    private void FixedUpdate()
    {
        OnJoystickMovement();
    }

    public void OnJoystickMovement()
    {
        Vector3 input = JoystickVariable.Value;
        if (input.magnitude < minDistanceMovementVariable.Value)
        {
            OnRaiseCharacterIdleEvent();
            rigidbody.linearVelocity = new Vector3(0f, rigidbody.linearVelocity.y, 0f);
            return;
        }

        OnRaiseCharacterMoveEvent();
        Vector3 newVelocity = input.normalized * (characterMoveSpeedVariable.Value * input.magnitude);
        newVelocity.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = newVelocity;
        Vector3 lookDirection = new Vector3(input.x, 0f, input.z);
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, characterRotateSpeedVariable.Value * Time.deltaTime);
        }

        OwnerPositionVariable.Value = transform.position;
    }

    void OnRaiseCharacterMoveEvent()
    {
        if (characterMovement != ECharacterMovement.Moving) characterMovement = ECharacterMovement.Moving;
        if (characterStateVariable.Value == ECharacterState.Carrying) onCharacterMoveCarryEvent.Raise();
        else onCharacterMoveEvent.Raise();
    }

    void OnRaiseCharacterIdleEvent()
    {
        if (characterMovement != ECharacterMovement.Idle) characterMovement = ECharacterMovement.Idle;
        if (characterStateVariable.Value == ECharacterState.Carrying) onCharacterIdleCarryEvent.Raise();
        else onCharacterIdleEvent.Raise();
    }
}

public enum ECharacterState
{
    NonCarry,
    Carrying,
}

public enum ECharacterMovement
{
    Idle,
    Moving,
}