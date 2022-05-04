using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
    [SerializeField] private Game_View game_View;
    [SerializeField] private Game_Model game_Model;
    [SerializeField] private Score_Controller Score_Controller;
    private float matchTime;
    private bool matchEnded = false;

    void Start()
    {
        matchTime = game_Model.matchTime;
    }

    void Update()
    {
        TimeUpdate();
        EndGame();
    }

    private void EndGame()
    {
        if (matchTime <= 0 && matchEnded == false)
        {
            Score_Controller.EndGame.Invoke();
            game_Model.endGamePanel.SetActive(true);
            Time.timeScale = 0;
            matchEnded = true;
        }
    }

    private void TimeUpdate()
    {
        matchTime -= Time.deltaTime;
        game_View.SetTimer(matchTime);
    }

    public void ResetGame() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
