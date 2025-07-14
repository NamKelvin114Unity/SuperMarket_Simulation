using UnityEngine;
using VirtueSky.Inspector;
using VirtueSky.Variables;

public class PlayerMovement : BaseCharacterMovement, IJoystickMovement
{
    [HeaderLine("Variable")] [SerializeField]
    private Vector3Variable joystickVariable;

    [SerializeField] FloatVariable maxSpeedParticleVariable;

    [HeaderLine("Properties")] [SerializeField]
    private Transform model;

    [SerializeField] PlayerAnimationController playerAnimationController;

    [SerializeField] private Vector3Variable ownerPositionVariable;
    [SerializeField] ParticleEffectController particleEffectController;


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
            particleEffectController.SetStopParticle();
            rigidbody.linearVelocity = new Vector3(0f, rigidbody.linearVelocity.y, 0f);
            return;
        }

        OnRaiseCharacterMoveEvent();
        var speed = characterMoveSpeedVariable.Value * input.magnitude;
        Vector3 newVelocity = input.normalized * (speed);
        var ratioParticleSpeed = speed / characterMoveSpeedVariable.Value * maxSpeedParticleVariable.Value;
        particleEffectController.SetSpeedParticle(ratioParticleSpeed);
        newVelocity.y = rigidbody.linearVelocity.y;
        rigidbody.linearVelocity = newVelocity;
        Vector3 lookDirection = new Vector3(input.x, 0f, input.z);
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            model.rotation = Quaternion.Slerp(model.rotation, targetRotation,
                characterRotateSpeedVariable.Value * Time.deltaTime);
        }

        OwnerPositionVariable.Value = transform.position;
    }

    void OnRaiseCharacterMoveEvent()
    {
        if (characterMovement != ECharacterMovement.Moving) characterMovement = ECharacterMovement.Moving;
        if (characterStateVariable.Value == ECharacterState.Carrying) playerAnimationController.OnPlayRunCarryAnim();
        else playerAnimationController.OnPlayRunAnim();
    }

    void OnRaiseCharacterIdleEvent()
    {
        if (characterMovement != ECharacterMovement.Idle) characterMovement = ECharacterMovement.Idle;
        if (characterStateVariable.Value == ECharacterState.Carrying) playerAnimationController.OnPlayIdleCarryAnim();
        else playerAnimationController.OnPlayIdleAnim();
    }
}

public enum ECharacterState
{
    NonCarry,
    Carrying,
    GoDestination,
    Idle,
    Waiting,
}

public enum ECharacterMovement
{
    Idle,
    Moving,
}