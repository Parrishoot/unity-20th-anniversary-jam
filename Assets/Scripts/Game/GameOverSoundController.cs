using System;
using UnityEngine;

public class GameOverSoundController : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private GameManager gameManager;

    [SerializeField]
    private AudioClip successClip;

    [SerializeField]
    private AudioClip failClip;

    [SerializeField]
    private SceneFadeController sceneFadeController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager.GameEnded += PlaySound;
    }

    private void PlaySound(GameManager.GameState state)
    {
        if (state == GameManager.GameState.WON && successClip != null)
        {
            audioSource.PlayOneShot(successClip);
        }
        else if (state == GameManager.GameState.LOST && failClip != null)
        {
            audioSource.PlayOneShot(failClip);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GameInProgress)
        {
            return;
        }

        audioSource.volume = 1 - sceneFadeController.FadeAmount;
    }
}
