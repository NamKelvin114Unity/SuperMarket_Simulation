using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;

public class CharacterIdleState : State
{
    public CharacterIdleState(GameObject owner, StateMachine stateMachine,
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