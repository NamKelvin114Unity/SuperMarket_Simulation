using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;

public abstract class State
{
    protected GameObject owner;
    protected HandleAnimancerComponent animancerComponent;
    protected AnimationClip animationClip;
    protected StateMachine stateMachine;

    public State(GameObject owner, StateMachine stateMachine, HandleAnimancerComponent animancerComponent,
        AnimationClip animationClip)
    {
        this.owner = owner;
        this.stateMachine = stateMachine;
        this.animancerComponent = animancerComponent;
        this.animationClip = animationClip;
    }

    public virtual void Enter(Action complete)
    {
        animancerComponent.PlayAnim(animationClip, complete, _durationFade: 0);
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