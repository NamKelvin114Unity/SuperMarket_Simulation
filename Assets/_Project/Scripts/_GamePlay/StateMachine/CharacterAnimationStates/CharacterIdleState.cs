using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;

public class CharacterIdleState : State
{
    public CharacterIdleState(GameObject owner, StateMachine stateMachine, HandleAnimancerComponent animancerComponent, ClipTransition animationClip)
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