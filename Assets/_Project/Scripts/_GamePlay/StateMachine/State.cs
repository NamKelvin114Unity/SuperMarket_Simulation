using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;

public abstract class State
{
    protected GameObject owner;
    protected HandleAnimancerComponentCustom AnimancerComponentCustom;
    protected AnimationClip animationClip;
    protected StateMachine stateMachine;

    public State(GameObject owner, StateMachine stateMachine, HandleAnimancerComponentCustom animancerComponentCustom,
        AnimationClip animationClip)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        this.AnimancerComponentCustom = animancerComponentCustom;
        this.animationClip = animationClip;
    }

    public virtual void Enter(Action complete)
    {
        AnimancerComponentCustom.PlayAnim(animationClip, complete, _durationFade: 0);
    }

    public virtual void Exit()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }
}