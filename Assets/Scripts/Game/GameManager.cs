using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LiveManager liveManager;

    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private SceneFadeController sceneFadeController;

    public enum GameState
    {
        WAITING,
        PLAYING,
        WON,
        LOST
    }

    public GameState State { get; private set; } = GameState.WAITING;

    public Action GameStarted { get; set; }

    public Action<GameState> GameEnded { get; set; }

    public bool GameInProgress
    {
        get
        {
            return State == GameState.WAITING || State == GameState.PLAYING;
        }
    }

    private void Awake()
    {
        liveManager.RanOutOfLives += GameOver;
        songManager.SongFinished += GameWon;

        sceneFadeController.OnFadeIn += StartGame;
    }

    private void GameOver()
    {
        GameEnded?.Invoke(GameState.LOST);
        State = GameState.LOST;
    }

    private void GameWon()
    {
        GameEnded?.Invoke(GameState.WON);
        State = GameState.WON;
    }

    void Start()
    {
        sceneFadeController.FadeIn();
    }
    
    private void StartGame()
    {
        GameStarted?.Invoke();
        State = GameState.PLAYING;
    }

    public void RestartGame()
    {
        if(!GameInProgress)
        {
            sceneFadeController.OnFadeOut += ReloadScene;
            sceneFadeController.FadeOut();
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    
    public void ReturnToMainMenu()
    {
        if(!GameInProgress)
        {
            sceneFadeController.OnFadeOut += LoadMenuScene;
            sceneFadeController.FadeOut();
        }
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
