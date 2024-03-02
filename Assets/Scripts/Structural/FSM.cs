using UnityEngine;

public class FSM
{
    State currentState;
    public FSM(State initialState)
        => currentState = initialState;

    public void SetState(State state)
    {
        currentState.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    public void UpdateState()
        => currentState.UpdateState();
}
