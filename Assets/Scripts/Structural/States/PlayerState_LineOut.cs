using System.Collections.Generic;
using UnityEngine;

public class PlayerState_LineOut : State
{
    Transform player;
    Bobber bobber;

    public PlayerState_LineOut(FSM fsm) : base(fsm) { }

    public override void OnStateEnter(Dictionary<string, object> blackboard)
    {

    }

    public override void OnStateExit(Dictionary<string, object> blackboard)
    {

    }

    public override void UpdateState(Dictionary<string, object> blackboard)
    {
        if (Input.GetMouseButton(0))
        {
            
        }
    }
}

