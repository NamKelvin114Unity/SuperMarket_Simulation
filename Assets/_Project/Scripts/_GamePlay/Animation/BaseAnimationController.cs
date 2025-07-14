using UnityEngine;
using UnityEngine.Serialization;
using VirtueSky.Inspector;

public abstract class BaseAnimationController : MonoBehaviour
{
    [FormerlySerializedAs("animancerComponent")] [HeaderLine("Core")] [SerializeField]
    protected HandleAnimancerComponentCustom animancerComponentCustom;

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