using System;
using Animancer;
using UnityEngine;
using VirtueSky.Component;
using VirtueSky.Inspector;

public abstract class BaseAnimationController : MonoBehaviour
{
    [HeaderLine("Core")] [SerializeField] protected HandleAnimancerComponent animancerComponent;

    [SerializeField] protected AnimationClip idleAnim;
    protected StateMachine stateMachine;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        OnStartState();
    }

    protected virtual void Initialize()
    {
        stateMachine = new StateMachine();
    }

    protected virtual void OnStartState()
    {
    }

    private void Update()
    {
        OnUpdateStateMachine();
    }

    protected virtual void OnUpdateStateMachine()
    {
        stateMachine.LogicUpdate();
    }
}