using KBGDLib.Communicators;
using System;
using UnityEngine;

public class BobberManager : MonoBehaviour, IService, ISubscriber
{
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] LineRenderer lineRenderer;

    [SerializeField] Vector2 defaultPosition = Vector2.zero;

    MessageBroker msgBroker;
    private void Awake()
    {
        ServiceLocator.RequestService("MessageBroker", out IService outService);
        if (outService != null)
            msgBroker = (MessageBroker)outService;
    }

    private void Start()
    {
        SetDefaultPosition(transform.position);
    }

     public Vector2 GetPosition()
        => transform.position;

    void SetDefaultPosition(Vector2 newPos)
    { 
        defaultPosition = newPos;
        SendDefaultPositionUpdateMSG();
    }

    void AddImmediateForceToBobber(Vector2 power)
        => rigidBody.AddForce(power);

    void ResetBobberPosition()
    {
        transform.position = defaultPosition;
        SendDefaultPositionUpdateMSG();
    }

     void ResetBobberVelocity()
        => rigidBody.velocity = Vector2.zero;

     void ResetBobber()
    {
        ResetBobberVelocity();
        ResetBobberPosition();
    }

    private void SendDefaultPositionUpdateMSG()
    {
        MSG_BobberDefaultPositionUpdated msg = new MSG_BobberDefaultPositionUpdated(
            "bobberDefaultPositionUpdated", defaultPosition);
        msgBroker.SendMessage(msg);
    }

    public bool Receive(Message msg)
    {
        if (msg.MessageType == "bobberReset")
        {
            ResetBobber();
            return true;
        }
        else  if (msg.MessageType == "applyForce")
        {
            MSG_CastLine castLine = (MSG_CastLine)msg;
            AddImmediateForceToBobber(castLine.power);
            return true;
        }

        return false;
    }
}
