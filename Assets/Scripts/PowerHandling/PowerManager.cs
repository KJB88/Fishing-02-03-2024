using KBGDLib.Communicators;
using UnityEngine;

public class PowerManager : MonoBehaviour, IService, ISubscriber
{
    [SerializeField] PowerView _view;
    [SerializeField] float maxPower;
    public float MaxPower => maxPower;
    [SerializeField] float powerMod;
    public float PowerMod => powerMod;

    MessageBroker msgBroker;

    // Start is called before the first frame update
    void Awake()
    { 
        ServiceLocator.AddService("PowerManager", this);
        ServiceLocator.RequestService("MessageBroker", out IService service);
        if (service != null)
            msgBroker = (MessageBroker)service;
    }

    public float AccumulatePower(float power)
    {
        // Power accumulator
        power += (2.5f * powerMod) * Time.deltaTime;
        power = Mathf.Clamp(power, 1.0f, maxPower * powerMod);

        // Normalize & update UI
        float normalizedVal = NormalizePowerValue(power);
        UpdatePowerUI(normalizedVal);

        return power;
    }

    private float NormalizePowerValue(float power)
        =>  power / (maxPower * powerMod);

    void UpdateLineRenderer(int index, Vector2 pos)
        => _view.LineRenderer.SetPosition(index, pos);

    void UpdatePowerUI(float normalizedPower)
    {
        _view.PowerBar.value = normalizedPower;
        _view.PowerText.text = (normalizedPower * 10.0f).ToString();

    }
    public bool Receive(Message msg)
    {
        if (msg.MessageType == "bobberDefaultPositionUpdated")
            return true;

        return false;
    }
}
