using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] Bobber bobber;
    [SerializeField] PowerView _view;
    Camera mainCam;

    bool held = false;
    float power = 0.0f;
    float maxPower = 20.0f;
    float powerMod = 10.0f;
    Vector2 dir = Vector2.zero;
    Vector2 diff = Vector2.zero;
    Vector2 clickPoint;

    private void Start()
    {
        mainCam = Camera.main; // Cache camera
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

        bobber.UpdateLineRendererPosition(0, bobber.transform.position);
    }

    private void AccumulatePower()
    {
        // Power acc.
        power += (2.5f *powerMod) * Time.deltaTime;
        power = Mathf.Clamp(power, 1.0f, maxPower * powerMod);

        float normalizedVal = power / (maxPower * powerMod);

        // UI
        _view.PowerBar.value = normalizedVal;
        _view.PowerText.text = power.ToString();

        // LineRenderer
        bobber.UpdateLineRendererPosition(1, (Vector2)bobber.transform.position + (diff * normalizedVal));
        //lineRenderer.SetPosition(1, clickPoint); // Static line

    }

    private void CastLine()
    {
        Debug.Log("MB Up!");
        held = false;
        bobber.AddImmediateForce(dir * power);
        power = 0.0f;
    }

    private void Reset()
    {
        Debug.Log("Resetting!");
        bobber.ResetBobber();
    }

    void CalculateDirection()
    {
        clickPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);

        diff = clickPoint - bobber.GetPosition();
        dir = diff.normalized;
        Debug.Log("DIR: " + dir);
    }
}
