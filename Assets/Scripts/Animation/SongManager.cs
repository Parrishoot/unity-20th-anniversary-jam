using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class SongManager : MonoBehaviour
{
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
    private GameProfile defaultProfile;

    public Action PuzzleThresholdPassed { get; set; }

    public Action SongFinished { get; set; }

    private float previousTime = -1f;

    private bool gameOver = false;

    public float PercentageOfSection
    {
        get
        {
            return TimeInSection / PuzzleTime;
        }
    }

    public float RemainingTime
    {
        get
        {
            return currentProfile.Song.length - ElapsedTime;
        }
    }

    public float ElapsedTime
    {
        get
        {
            if (gameManager.State == GameManager.GameState.WAITING)
            {
                return 0f;
            }
            

            return Time.time - startTime;
        }
    }

    public float ElaspedPercentage
    {
        get
        {
            return ElapsedTime / currentProfile.Song.length;
        }
    }

    public float TimeInSection
    {
        get
        {
            return Mathf.Repeat(ElapsedTime, PuzzleTime);
        }
    }

    public float PuzzleTime
    {
        get
        {
            return currentProfile.BeatsPerPuzzle * (1f / (CurrentBPM / 60f));
        }
    }

    public bool HasTimeForPuzzle
    {
        get
        {
            return RemainingTime > PuzzleTime;
        }
    }

    private GameProfile currentProfile;

    public float CurrentBPM
    {
        get
        {
            return Mathf.Lerp(currentProfile.MinBPM, currentProfile.MaxBPM, ElaspedPercentage);
        }
    }

    private float startTime = 0f;

    private void Awake()
    {
        SetProfile();

        gameManager.GameStarted += PlaySong;
        gameManager.GameEnded += (x) =>
        {
            sourcePool.First().DOFade(0f, 1f);
            gameOver = true;
        };
    }

    private void SetProfile()
    {
        GameProfileManager gameProfileManager = FindAnyObjectByType<GameProfileManager>();
        if (gameProfileManager == null)
        {
            currentProfile = defaultProfile;
        }
        else
        {
            currentProfile = gameProfileManager.ActiveProfile;   
        }
    }

    private void PlaySong()
    {
        sourcePool.First().clip = currentProfile.Song;
        sourcePool.First().Play();

        startTime = Time.time;
    }

    private void Update()
    {
        // Debug.Log($"Previous: {previousTime}, Current: {PercentageOfSection}");
    
        if (gameOver || gameManager.State == GameManager.GameState.WAITING)
        {
            return;
        }

        if (previousTime > TimeInSection)
        {
            // Debug.Log($"Threshold Passed Finished!");
            PuzzleThresholdPassed?.Invoke();
        }

        if (!sourcePool.First().isPlaying && gameManager.State == GameManager.GameState.PLAYING)
        {
            SongFinished?.Invoke();
        }

        previousTime = TimeInSection;
    }

    private void FixedUpdate()
    {
        if (gameOver || gameManager.State == GameManager.GameState.WAITING)
        {
            return;
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
    }
}
