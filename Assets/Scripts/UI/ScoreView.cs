using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour, ISubscriber
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] string scorePrefix = "Score: ";
    [SerializeField] float currentScore = 0;
    private void Start()
    {
        MessageBroker.RegisterSubscriber("FishCaught", this);
    }

    void UpdateScore(float score)
    {
        currentScore += score;
        scoreText.text = scorePrefix + currentScore.ToString();
    }

    public bool Receive(Message msg)
    {
        if (msg.MessageType == "FishCaught")
        {
            MSG_ScoreUpdated scoreUpd = (MSG_ScoreUpdated)msg;
            UpdateScore(scoreUpd.score);
        }

        return false;
    }
}
