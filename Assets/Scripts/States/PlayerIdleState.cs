using KBGDLib.Structural;

public class PlayerIdleState : State
{
    public PlayerIdleState(string stateName, FiniteStateMachine fsm)
        : base(stateName, fsm) { }

    public override void OnStateEntry() { }

    public override void OnStateExit() { }

    public override void UpdateState(Blackboard blackboard)
    {

    }

}
