using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour, ISubscriber
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] string scorePrefix = "Score: ";

    void UpdateScore(float score)
        => scoreText.text = scorePrefix + score;

    public bool Receive(Message msg)
    {
        if (msg.MessageType == "ScoreUpdated")
        {
            MSG_ScoreUpdated scoreUpd = (MSG_ScoreUpdated)msg;
            UpdateScore(scoreUpd.score);
        }

        return true;
    }
}
