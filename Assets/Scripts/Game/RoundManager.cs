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

    private bool roundSuccess = true;

    private void Start()
    {
        gridController.SuccessfulGrid += () => roundSuccess = true;
        ResetTimer();
    }

    private void ResetTimer()
    {
        if (CountdownTimer != null)
        {
            TimerManager.DeregisterTimer(CountdownTimer);
        }
        
        if(!roundSuccess)
        {
            failedAudioSource.Play();
            cameraController.Shake(CameraController.ShakeType.HEAVY);
        }

        gridController.RandomizeGrid();
        roundSuccess = false;
        CountdownTimer = TimerManager.RegisterTimer(roundLength, ResetTimer);
    }
}
