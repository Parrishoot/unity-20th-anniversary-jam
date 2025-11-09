using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> songOrder;

    [SerializeField]
    List<AudioSource> sourcePool;

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

    private float previousPercentage = -1f;

    public float PercentageOfSection
    {
        get
        {
            return Mathf.Repeat(sourcePool.First().time, secondsPerPuzzle) / secondsPerPuzzle;
        }
    }

    void Awake()
    {
        secondsPerPuzzle = beatsPerPuzzle * (1 / (bpm / 60));
        Debug.Log($"Seconds Per Puzzle: {secondsPerPuzzle}");
    }

    private void Start()
    {
        sourcePool.First().Play();
    }

    private void Update()
    {
        if(previousPercentage > PercentageOfSection)
        {
            PuzzleThresholdPassed?.Invoke();
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
