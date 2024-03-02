using KBGDLib.Communicators;
using UnityEngine;

public class MSG_BobberDefaultPositionUpdated : Message
{
    public Vector2 newPos;
    public MSG_BobberDefaultPositionUpdated(string messageType, Vector2 newPos) 
        : base(messageType) => this.newPos = newPos;
}
