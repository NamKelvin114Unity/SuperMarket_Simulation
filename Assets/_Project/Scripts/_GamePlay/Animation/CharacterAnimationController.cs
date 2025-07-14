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
        stateMachine.ChangeState(new CharacterIdleCarryState(gameObject, stateMachine, animancerComponentCustom,
            animCarryIdle));
    }

    public void OnPlayRunCarryAnim()
    {
        stateMachine.ChangeState(
            new CharacterMoveCarryState(gameObject, stateMachine, animancerComponentCustom, animCarryRun));
    }

    public void OnPlayRunAnim()
    {
        stateMachine.ChangeState(new CharacterMoveState(gameObject, stateMachine, animancerComponentCustom, animRun));
    }

    public void OnPlayIdleAnim()
    {
        stateMachine.ChangeState(new CharacterIdleState(gameObject, stateMachine, animancerComponentCustom, idleAnim));
    }

    protected override void OnStartState()
    {
        base.OnStartState();
        stateMachine.Initialize(new CharacterIdleState(gameObject, stateMachine, animancerComponentCustom, idleAnim));
    }
}