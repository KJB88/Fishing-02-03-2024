using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerState_ReadyToCast : State
{
    // Dependencies
    Bobber bobber;
    Transform cursor;
    PowerView _view;
    // Cache main for perf
    Camera mainCam;

    // Working
    Vector2 clickPoint;
    Vector2 diff;

    public PlayerState_ReadyToCast(FSM fsm) : base(fsm)
        => mainCam = Camera.main;

    public override void OnStateEnter(Dictionary<string, object> blackboard)
    {
        //Debug.Log("Entering PlayerState_ReadyToCast");
        object obj;
        if (blackboard.TryGetValue("bobber", out obj))
            bobber = (Bobber)obj;

        if (blackboard.TryGetValue("cursor", out obj))
            cursor = (Transform)obj;

        if (blackboard.TryGetValue("clickPoint", out obj))
            clickPoint = (Vector2)obj;

        if (blackboard.TryGetValue("diff", out obj))
            diff = (Vector2)obj;

        if (blackboard.TryGetValue("powerView", out obj))
            _view = (PowerView)obj;

        _view.PowerText.text = "Hold LMB to power up your cast!";
        Reset();
    }

    public override void OnStateExit(Dictionary<string, object> blackboard) { }

    public override void UpdateState(Dictionary<string, object> blackboard)
    {
        // Active trigger
        if (Input.GetMouseButtonDown(0))
        {
            BeginCasting(blackboard);
            fsm.SetState(new PlayerState_Casting(fsm), blackboard);
        }
    }

    private void Reset()
        => bobber.ResetBobber();

    private void BeginCasting(Dictionary<string, object> blackboard)
    {
        // Register click point to lock it in
        clickPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);
        if (blackboard.TryGetValue("clickPoint", out _))
            blackboard["clickPoint"] = clickPoint;
        else
            blackboard.Add("clickPoint", clickPoint);

        // Activate landing cursor
        cursor.gameObject.SetActive(true);
        cursor.position = clickPoint;

        diff = clickPoint - bobber.GetDefaultPosition();
        if (blackboard.TryGetValue("diff", out _))
            blackboard["diff"] = diff;
        else
            blackboard.Add("diff", diff);

        bobber.UpdateLineRendererPosition(0, bobber.GetPosition());
    }
}

