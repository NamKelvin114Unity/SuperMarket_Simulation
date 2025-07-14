using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;

public class CharacterIdleCarryState : State
{
    public CharacterIdleCarryState(GameObject owner, StateMachine stateMachine,
        HandleAnimancerComponent animancerComponent, AnimationClip animationClip)
        : base(owner, stateMachine, animancerComponent, animationClip)
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