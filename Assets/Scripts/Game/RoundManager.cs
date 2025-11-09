using System;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private float roundLength = 3f;

    [SerializeField]
    private float roundBuffer = .5f;

    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private AudioSource failedAudioSource;

    [SerializeField]
    private SongManager songManager;

    public Timer SuccessCountdownTimer { get; private set; }

    public Timer FailCountdownTimer { get; private set; }

    public Action RoundFailed { get; set; }

    public RoundState RoundState { get; private set; }

    public RoundState PreviousRoundState { get; private set; }

    private void Start()
    {
        gridController.SuccessfulGrid += () => RoundState = RoundState.PASSED;
        songManager.PuzzleThresholdPassed += ResetTimer;

        PreviousRoundState = RoundState.PASSED;
        RoundState = RoundState.WAITING;
        gridController.RandomizeGrid();
    }
    
    private void ProcessFailure()
    {
        failedAudioSource.Play();
        cameraController.Shake(CameraController.ShakeType.HEAVY);
        RoundFailed?.Invoke();
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
    }
}
