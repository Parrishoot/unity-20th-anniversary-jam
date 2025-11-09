using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> songOrder;

    [SerializeField]
    List<AudioSource> sourcePool;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private RoundManager roundManager;

    [SerializeField]
    private AnimationCurve pitchOnFailCurve;

    [SerializeField]
    private AnimationCurve volumeOnFailCurve;

    [SerializeField]
    private AnimationCurve slowdownCurve;

    [SerializeField]
    private float bpm = 180;

    [SerializeField]
    private float beatsPerPuzzle = 8;

    private float secondsPerPuzzle;

    public Action PuzzleThresholdPassed { get; set; }

    public Action SongFinished { get; set; }

    private float previousPercentage = -1f;

    private bool gameOver = false;

    public float PercentageOfSection
    {
        get
        {
            return TimeInSection / secondsPerPuzzle;
        }
    }

    public float RemainingTime
    {
        get
        {
            return sourcePool.First().clip.length - sourcePool.First().time;
        }
    }

    public float TimeInSection
    {
        get
        {
            return Mathf.Repeat(sourcePool.First().time, secondsPerPuzzle);
        }
    }

    public float PuzzleTime
    {
        get
        {
            return secondsPerPuzzle;
        }
    }

    public bool HasTimeForPuzzle
    {
        get
        {
            return RemainingTime > secondsPerPuzzle;
        }
    }

    private void Awake()
    {
        secondsPerPuzzle = beatsPerPuzzle * (1 / (bpm / 60));
        Debug.Log($"Seconds Per Puzzle: {secondsPerPuzzle}");

        gameManager.GameStarted += () => sourcePool.First().Play();
        gameManager.GameEnded += (x) =>
        {
            sourcePool.First().DOFade(0f, 1f);
            gameOver = true;
        };
    }

    private void Update()
    {
        if(gameOver || gameManager.State == GameManager.GameState.WAITING)
        {
            return;
        }

        if (previousPercentage > PercentageOfSection)
        {
            PuzzleThresholdPassed?.Invoke();
        }
        
        if(!sourcePool.First().isPlaying && gameManager.State == GameManager.GameState.PLAYING)
        {
            SongFinished?.Invoke();
        }

        if (roundManager.PreviousRoundState == RoundState.FAILED && PercentageOfSection < .5f)
        {
            sourcePool.First().pitch = pitchOnFailCurve.Evaluate(PercentageOfSection);
            sourcePool.First().volume = volumeOnFailCurve.Evaluate(PercentageOfSection);
        }
        else if (roundManager.RoundState != RoundState.PASSED)
        {
            sourcePool.First().pitch = slowdownCurve.Evaluate(PercentageOfSection);
            sourcePool.First().volume = slowdownCurve.Evaluate(PercentageOfSection);
        }
        else
        {
            sourcePool.First().pitch = 1;
            sourcePool.First().volume = 1;
        }

        previousPercentage = PercentageOfSection;
    }
}
