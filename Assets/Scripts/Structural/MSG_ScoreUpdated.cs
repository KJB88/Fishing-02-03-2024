using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSG_ScoreUpdated : Message
{
    public float score;
    public MSG_ScoreUpdated(string messageType, float score) : base(messageType)
        => this.score = score;
}
