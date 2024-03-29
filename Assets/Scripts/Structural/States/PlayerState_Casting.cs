using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerState_Casting : State
{
    // Dependencies
    PowerView _view;
    Bobber bobber;
    Transform cursor;

    // Working
    Vector2 diff;
    float power = 0.0f;
    float maxPower;
    float gainMod;
    float powerMod;
    float powerBase;

    bool held = true;

    public PlayerState_Casting(FSM fsm) : base(fsm) { }

    public override void OnStateEnter(Dictionary<string, object> blackboard)
    {
        //Debug.Log("Entering PlayerState_Casting");
        object obj;
        if (blackboard.TryGetValue("bobber", out obj))
            bobber = (Bobber)obj;
        if (blackboard.TryGetValue("powerView", out obj))
            _view = (PowerView)obj;
        if (blackboard.TryGetValue("cursor", out obj))
            cursor = (Transform)obj;

        if (blackboard.TryGetValue("maxPower", out obj))
            maxPower = (float)obj;
        if (blackboard.TryGetValue("gainMod", out obj))
            gainMod = (float)obj;
        if (blackboard.TryGetValue("powerMod", out obj))
            powerMod = (float)obj;
        if (blackboard.TryGetValue("powerBase", out obj))
            powerBase = (float)obj;

        if (blackboard.TryGetValue("diff", out obj))
            diff = (Vector2)obj;
    }

    public override void OnStateExit(Dictionary<string, object> blackboard)
    {
        _view.PowerBar.value = 0;
        _view.PowerText.text = "";
    }

    public override void UpdateState(Dictionary<string, object> blackboard)
    {
        if (held)
            AccumulatePower();

        // Reset working vars
        if (Input.GetMouseButtonUp(0))
        {
            CastLine();
            held = false;
        }
    }

    private void AccumulatePower()
    {
        // Power acc.
        power += powerBase * (Time.deltaTime * gainMod);
        power = Mathf.Clamp(power, 1.0f, maxPower);

        float normalizedVal = power / (maxPower);

        // UI
        _view.PowerBar.value = normalizedVal;
        _view.PowerText.text = power.ToString();

        // LineRenderer
        bobber.UpdateLineRendererPosition(1, (Vector2)bobber.transform.position + (diff * normalizedVal));
    }

    private void CastLine()
    {
        // Calculate force
        Vector2 dir = diff.normalized;
        bobber.AddImmediateForce(dir * (power * powerMod));

        // Reset power
        power = 0.0f;

        // Disable cursor
        cursor.gameObject.SetActive(false);
    }
}
