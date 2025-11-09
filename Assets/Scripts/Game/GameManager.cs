using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private LiveManager liveManager;

    [SerializeField]
    private SongManager songManager;

    public enum GameState
    {
        PLAYING,
        WON,
        LOST
    }

    private GameState gameState;

    public Action GameStarted { get; set; }

    public Action<GameState> GameEnded { get; set; }

    private void Awake()
    {
        liveManager.RanOutOfLives += GameOver;
        songManager.SongFinished += GameWon;
    }

    private void GameOver()
    {
        GameEnded?.Invoke(GameState.LOST);
        gameState = GameState.LOST;
    }

    private void GameWon()
    {
        GameEnded?.Invoke(GameState.WON);
        gameState = GameState.WON;
    }

    void Start()
    {
        GameStarted?.Invoke();
        gameState = GameState.PLAYING;
    }

    void Update()
    {
        if(gameState != GameState.PLAYING && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
