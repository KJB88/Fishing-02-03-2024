using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{ 
    [SerializeField] Bobber bobber;
    [SerializeField] PowerView _view;
    [SerializeField] Transform cursor;

    Camera mainCam;

    bool held = false;
    float power = 0.0f;
    float maxPower = 20.0f;
    float powerMod = 10.0f;
    float gainMod = 2.0f;

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
        // Register click point to lock it in
        clickPoint = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Activate landing cursor
        cursor.gameObject.SetActive(true);
        cursor.position = clickPoint;

        diff = clickPoint - bobber.GetPosition();

        // Begin accummulator
        held = true;

        bobber.UpdateLineRendererPosition(0, bobber.GetPosition());
    }

    private void AccumulatePower()
    {
        // Power acc.
        power += (2.5f *powerMod) * (Time.deltaTime * gainMod);
        power = Mathf.Clamp(power, 1.0f, maxPower * powerMod);

        float normalizedVal = power / (maxPower * powerMod);

        // UI
        _view.PowerBar.value = normalizedVal;
        _view.PowerText.text = power.ToString();

        // LineRenderer
        bobber.UpdateLineRendererPosition(1, (Vector2)bobber.transform.position + (diff * normalizedVal));

    }

    private void CastLine()
    {
        // Deactivate accummulator
        held = false;

        // Calculate voice
        Vector2 dir = diff.normalized;
        bobber.AddImmediateForce(dir * power);

        // Reset power
        power = 0.0f;

        // Disable cursor
        cursor.gameObject.SetActive(false);
    }

    private void Reset()
    {
        bobber.ResetBobber();
    }
}
