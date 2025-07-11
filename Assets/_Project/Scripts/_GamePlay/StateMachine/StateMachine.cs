using System;

public class StateMachine
{
    public State CurrentState { get; private set; }

    public void Initialize(State startingState, Action completeAnim = null)
    {
        CurrentState = startingState;
        CurrentState.Enter(completeAnim);
    }

    public void ChangeState(State newState, Action completeAnim = null)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter(completeAnim);
    }

    public void LogicUpdate()
    {
        CurrentState?.LogicUpdate();
    }

    public void PhysicsUpdate()
    {
        CurrentState?.PhysicsUpdate();
    }
}