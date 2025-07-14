using Animancer;
using UnityEngine;
using VirtueSky.Inspector;

public abstract class CharacterAnimationController : BaseAnimationController
{
    [HeaderLine("ClipTransitions")] [SerializeField]
    private AnimationClip animRun;

    [SerializeField] private AnimationClip animCarryRun;
    [SerializeField] private AnimationClip animCarryIdle;

    public void OnPlayIdleCarryAnim()
    {
        stateMachine.ChangeState(new CharacterIdleCarryState(gameObject, stateMachine, animancerComponent,
            animCarryIdle));
    }

    public void OnPlayRunCarryAnim()
    {
        stateMachine.ChangeState(
            new CharacterMoveCarryState(gameObject, stateMachine, animancerComponent, animCarryRun));
    }

    public void OnPlayRunAnim()
    {
        stateMachine.ChangeState(new CharacterMoveState(gameObject, stateMachine, animancerComponent, animRun));
    }

    public void OnPlayIdleAnim()
    {
        stateMachine.ChangeState(new CharacterIdleState(gameObject, stateMachine, animancerComponent, idleAnim));
    }

    protected override void OnStartState()
    {
        base.OnStartState();
        stateMachine.Initialize(new CharacterIdleState(gameObject, stateMachine, animancerComponent, idleAnim));
    }
}