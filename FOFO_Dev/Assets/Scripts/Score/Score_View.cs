using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Score_View : MonoBehaviour
{
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highestScoreText;

    public void SetCurrentScore(int currentScore)
    {
        currentScoreText.text = currentScore.ToString();
    }
    public void SetHighScore(int highScore) 
    {
        highestScoreText.text = highScore.ToString();
    }
  
}
