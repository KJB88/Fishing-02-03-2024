using TMPro;
using UnityEngine;
using UnityEngine.UI;
using KBGDLib.Structural;
using KBGDLib.Communicators;

public class PlayerController : MonoBehaviour
{
    FiniteStateMachine fsm;
    Blackboard playerBlackboard;

    // State Library
    [SerializeField] BobberManager bobber;
    PowerManager powerManager;

    bool held = false;
    float power = 0.0f;
    float maxPower = 20.0f;
    float powerMod = 10.0f;
    Vector2 dir = Vector2.zero;
    Vector2 diff = Vector2.zero;
    Vector2 clickPoint;

    private void Start()
    {
        ServiceLocator.RequestService("PowerManager", out IService service);
        if (service != null)
        {
            powerManager = (PowerManager)service;
        }


    }

    void Update()
    {
        // Active trigger
        if (Input.GetMouseButtonDown(0))
        {
            Reset();
            BeginCasting();
        }

        // Accumulator
        if (held)
            AccumulatePower();

        // Reset working vars
        if (Input.GetMouseButtonUp(0))
            CastLine();

        // Reset
        if (Input.GetKeyDown(KeyCode.Space))
            Reset();
    }

    private void BeginCasting()
    {
        CalculateDirection();

        Debug.Log("MB Down!");
        held = true;

    }

    private void AccumulatePower()
    {
        // Power acc.
        power += (2.5f *powerMod) * Time.deltaTime;
        power = Mathf.Clamp(power, 1.0f, maxPower * powerMod);

        float normalizedVal = power / (maxPower * powerMod);

        // UI
        powerBar.value = normalizedVal;
        text.text = power.ToString();

        // LineRenderer
        bobber.SetLineRendererPosition(1, bobber.GetPosition() + (diff * normalizedVal));
    }

    private void CastLine()
    {
        Debug.Log("MB Up!");
        held = false;
        bobber.AddImmediateForceToBobber(dir * power);
        power = 0.0f;
    }

    private void Reset()
    {
        Debug.Log("Resetting!");
        bobber.ResetBobber();
    }

    void CalculateDirection()
    {
        clickPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        diff = clickPoint - bobber.GetPosition();
        dir = diff.normalized;
        Debug.Log("DIR: " + dir);
    }
}
