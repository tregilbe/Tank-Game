using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public TankData player;
    public ScoreData playerScoreData;

    void AddScoreToHighScores()
    {
        GameManager.Instance.scores.Add(playerScoreData);
    }

    void Update()
    {
        playerScoreData.score = player.Score;
    }
}
