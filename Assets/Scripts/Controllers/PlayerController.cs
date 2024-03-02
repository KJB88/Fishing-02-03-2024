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

    readonly Dictionary<string, object> blackboard
        = new Dictionary<string, object>();

    [Tooltip("The maximum power achievable.")]
    [SerializeField] float maxPower = 20.0f;
    [Tooltip("The factor by which the overall power output is multiplied by.")]
    [SerializeField] float powerMod = 5.0f;
    [Tooltip("The rate of gain by which the power bar fills up.")]
    [SerializeField] float gainMod = 2.0f;
    [Tooltip("The default base rate of power.")]
    [SerializeField] float powerBase = 2.0f;
    private void Start()
    {
        fsm = new FSM(new PlayerState_ReadyToCast(fsm)); // TODO: Cache states

        blackboard.Add("bobber", bobber);
        blackboard.Add("powerView", _view);
        blackboard.Add("cursor", cursor);

        blackboard.Add("powerBase", gainMod);
        blackboard.Add("maxPower", maxPower);
        blackboard.Add("powerMod", powerMod);
        blackboard.Add("gainMod", gainMod);
    }

    void Update()
    {
        fsm.UpdateState();

        if (Input.GetKeyDown(KeyCode.Space))
            Reset();
    }

    private void Reset()
    {
        fsm.SetState(new PlayerState_ReadyToCast(fsm));
    }
}
