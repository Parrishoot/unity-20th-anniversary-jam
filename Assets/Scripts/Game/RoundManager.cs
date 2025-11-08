using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private float roundLength = 2f;

    [SerializeField]
    private GridController gridController;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    private AudioSource failedAudioSource;

    public Timer CountdownTimer { get; private set; }

    private void Start()
    {
        gridController.SuccessfulGrid += ResetTimer;
        ResetTimer();
    }

    private void ResetTimerFailed()
    {
        failedAudioSource.Play();
        cameraController.Shake(CameraController.ShakeType.HEAVY);
        ResetTimer();
    }

    private void ResetTimer()
    {
        if(CountdownTimer != null)
        {
            TimerManager.DeregisterTimer(CountdownTimer);
        }

        gridController.RandomizeGrid();
        CountdownTimer = TimerManager.RegisterTimer(roundLength, ResetTimerFailed);
    }
}
