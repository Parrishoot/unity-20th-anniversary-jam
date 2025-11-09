using UnityEngine;

public class MenuSoundController : MonoBehaviour
{
    [SerializeField]
    private SceneFadeController sceneFadeController;

    [SerializeField]
    private AudioSource audioSource;

    private float startingVolume;

    void Start()
    {
        startingVolume = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = Mathf.Lerp(startingVolume, 0f, sceneFadeController.FadeAmount);
    }
}
