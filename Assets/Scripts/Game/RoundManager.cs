using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{

    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private AudioSource failedAudioSource;

    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private GameManager gameManager;

    public Action<RoundState> RoundEnded { get; set; }

    public RoundState RoundState { get; private set; }

    public RoundState PreviousRoundState { get; private set; }

    private List<float> solveTimes = new List<float>();

    private void Start()
    {
        gridController.SuccessfulGrid += () =>
        {
            RoundState = RoundState.PASSED;
            solveTimes.Add(songManager.TimeInSection);
        };

        songManager.PuzzleThresholdPassed += ResetTimer;

        gameManager.GameStarted += () =>
        {
            PreviousRoundState = RoundState.PASSED;
            RoundState = RoundState.WAITING;
            gridController.RandomizeGrid();
        };
    }
    
    private void ProcessFailure()
    {
        failedAudioSource.Play();
        cameraController.Shake(CameraController.ShakeType.HEAVY);
    }

    public void ResetTimer()
    {
        if (RoundState == RoundState.WAITING)
        {
            PreviousRoundState = RoundState.FAILED;
            ProcessFailure();
        }
        else
        {
            PreviousRoundState = RoundState.PASSED;
        }

        RoundState = RoundState.WAITING;
        gridController.RandomizeGrid();

        RoundEnded?.Invoke(PreviousRoundState);
    }

    public float GetAverageSolveTime()
    {
        if (solveTimes.Count == 0)
        {
            return 0;
        }

        return solveTimes.Average();
    }
}
