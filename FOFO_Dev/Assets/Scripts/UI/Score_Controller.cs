using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Score_Controller : MonoBehaviour
{
    private const string HighestScore = "highestScore";

    [SerializeField] private Score_Model score_Model;
    [SerializeField] private Score_View score_View;

    public UnityEvent AddScore;
    public UnityEvent EndGame;

    private void Start()
    {
        GetHighScoreData();

        AddScore.AddListener(AddScoreEventHandler);
        EndGame.AddListener(EndGameEventHandler);
    }

    private void GetHighScoreData()
    {
        if (PlayerPrefs.HasKey(HighestScore))
        {
            score_Model.highestScore = PlayerPrefs.GetInt(HighestScore);
        }
        else
        {
            score_Model.highestScore = 0;
        }

        score_View.SetHighScore(score_Model.highestScore);
    }

    private void CheckHighScore() 
    {
        if (score_Model.currentScore > score_Model.highestScore)
        {
            PlayerPrefs.SetInt(HighestScore, score_Model.currentScore);
            score_View.SetHighScore(score_Model.currentScore);
        }
        else
        {
            return;
        }
    }

    private void AddScoreEventHandler() 
    {
        score_Model.currentScore++;
        score_View.SetCurrentScore(score_Model.currentScore);
    }

    private void EndGameEventHandler()
    {
        CheckHighScore();
    }
}
