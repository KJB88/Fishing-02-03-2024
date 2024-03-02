using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Bobber bobber;
    [SerializeField] PowerView _view;
    [SerializeField] Transform cursor;
    FSM fsm;

    Dictionary<string, object> blackboard;

    [Header("Casting Line")]
    [Tooltip("The maximum power achievable.")]
    [SerializeField] float maxPower = 20.0f;
    [Tooltip("The factor by which the overall power output is multiplied by.")]
    [SerializeField] float powerMod = 5.0f;
    [Tooltip("The rate of gain by which the power bar fills up.")]
    [SerializeField] float gainMod = 2.0f;
    [Tooltip("The default base rate of power.")]
    [SerializeField] float powerBase = 2.0f;

    [Header("Reeling In")]
    [SerializeField] float reelSpeed = 2.5f;
    [SerializeField] float reelPower = 2.0f;
    [SerializeField] float maxLineTension = 20.0f;

    private void Start()
    {
        blackboard = new Dictionary<string, object>
        {
            {"player", transform },
            { "bobber", bobber },
            { "powerView", _view },
            { "cursor", cursor },
            { "powerBase", gainMod },
            { "maxPower", maxPower },
            { "powerMod", powerMod },
            { "gainMod", gainMod },
            { "reelSpeed", reelSpeed },
            {"reelPower", reelPower },
            {"maxLineTension", maxLineTension }
        };

        fsm = new FSM();
        fsm.SetState(new PlayerState_ReadyToCast(fsm), blackboard);
    }

    void Update()
    {
        fsm.UpdateState(blackboard);

        if (Input.GetKeyDown(KeyCode.Space))
            Reset();
    }

    private void Reset()
    {
        fsm.SetState(new PlayerState_ReadyToCast(fsm), blackboard);
    }
}
