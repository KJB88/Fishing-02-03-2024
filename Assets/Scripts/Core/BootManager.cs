using KBGDLib.Communicators;
using UnityEngine;

public class BootManager : MonoBehaviour
{
    private void Awake()
    {
        ServiceLocator.AddService("MessageBroker", new MessageBroker());
    }
}
