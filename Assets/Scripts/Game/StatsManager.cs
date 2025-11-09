using System;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    [SerializeField]
    private LiveManager livesManager;

    [SerializeField]
    private RoundManager roundManager;

    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private GameOverUIController uiController;

    void Awake()
    {
        gameManager.GameEnded += ShowGameOverPanel;
    }

    private void ShowGameOverPanel(GameManager.GameState state)
    {
        uiController.ShowScreen(GetRank(), roundManager.GetAverageSolveTime(), livesManager.LivesRemaning);
    }

    public string GetRank()
    {
        int livesLost = livesManager.NumLives - livesManager.LivesRemaning;

        if(livesManager.LivesRemaning == 0)
        {
            return "F";
        }

        switch (livesLost)
        {
            case 2:
                return "C";
            case 1:
                return "B";
            case 0:
                return roundManager.GetAverageSolveTime() <= songManager.PuzzleTime * .5f ? "S" : "A";
            default:
                return "D";

        }
    }
}
