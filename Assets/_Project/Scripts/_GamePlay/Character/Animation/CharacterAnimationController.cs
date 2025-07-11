using Animancer;
using UnityEngine;
using VirtueSky.Events;
using VirtueSky.Inspector;

public class CharacterAnimationController : BaseAnimationController
{
    [HeaderLine("Event")] [SerializeField] private EventNoParam characterRunEvent;
    [SerializeField] private EventNoParam characterCarryIdleEvent;
    [SerializeField] private EventNoParam characterRunCarryEvent;
    [SerializeField] private EventNoParam characterIdleEvent;

    [HeaderLine("ClipTransitions")] [SerializeField]
    private ClipTransition animIdle;

    [SerializeField] private ClipTransition animRun;
    [SerializeField] private ClipTransition animCarryRun;
    [SerializeField] private ClipTransition animCarryIdle;

    private void OnEnable()
    {
        characterRunEvent.OnRaised += OnPlayRunAnim;
        characterCarryIdleEvent.OnRaised += OnPlayIdleCarryAnim;
        characterRunCarryEvent.OnRaised += OnPlayRunCarryAnim;
        characterIdleEvent.OnRaised += OnPlayIdleAnim;
    }

    private void OnDisable()
    {
        characterRunEvent.OnRaised -= OnPlayRunAnim;
        characterCarryIdleEvent.OnRaised -= OnPlayIdleCarryAnim;
        characterRunCarryEvent.OnRaised -= OnPlayRunCarryAnim;
        characterIdleEvent.OnRaised -= OnPlayIdleAnim;
    }

    void OnPlayIdleCarryAnim()
    {
        stateMachine.ChangeState(new CharacterIdleCarryState(gameObject, stateMachine, animancerComponent, animCarryIdle));
    }

    void OnPlayRunCarryAnim()
    {
        stateMachine.ChangeState(new CharacterMoveCarryState(gameObject, stateMachine, animancerComponent, animCarryRun));
    }

    void OnPlayRunAnim()
    {
        stateMachine.ChangeState(new CharacterMoveState(gameObject, stateMachine, animancerComponent, animRun));
    }

    void OnPlayIdleAnim()
    {
        stateMachine.ChangeState(new CharacterIdleState(gameObject, stateMachine, animancerComponent, idleAnim));
    }

    protected override void OnStartState()
    {
        base.OnStartState();
        stateMachine.Initialize(new CharacterIdleState(gameObject, stateMachine, animancerComponent, idleAnim));
    }
}