using System;
using UnityEngine;
using VirtueSky.Component;

public class CharacterMoveState : State
{
    public CharacterMoveState(GameObject owner, StateMachine stateMachine,
        HandleAnimancerComponentCustom animancerComponentCustom,
        AnimationClip animationClip)
        : base(owner, stateMachine, animancerComponentCustom, animationClip)
    {
    }

    public override void Enter(Action complete)
    {
        base.Enter(complete);
    }

    public override void LogicUpdate()
    {
    }

    public override void PhysicsUpdate()
    {
    }
}