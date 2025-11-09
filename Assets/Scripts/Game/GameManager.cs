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
        sceneFadeController.OnFadeOut += RestartGame;
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

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    void Update()
    {
        if(State != GameState.PLAYING && Input.GetKeyDown(KeyCode.Space))
        {
            sceneFadeController.FadeOut();
        }
    }
}
