using KBGDLib.Communicators;
using UnityEngine;

public class MSG_CastLine : Message
{
    public Vector2 power;

    public MSG_CastLine(string messageType, Vector2 power) 
        : base(messageType) => this.power = power;
}
