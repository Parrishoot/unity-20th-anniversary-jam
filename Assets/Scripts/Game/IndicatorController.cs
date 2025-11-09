using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    [SerializeField]
    private RoundManager roundManager;

    [SerializeField]
    private SongManager songManager;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private AnimationCurve fadeCurve;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Color waitingColor;

    [SerializeField]
    private Color warningColor;

    void Update()
    {
        Color color;

        if (roundManager.RoundState == RoundState.PASSED)
        {
            color = successColor;
        }
        else
        {
            color = Color.Lerp(waitingColor, warningColor, fadeCurve.Evaluate(songManager.PercentageOfSection));
        }
        
        color.a = fadeCurve.Evaluate(songManager.PercentageOfSection);
        transform.localScale = Vector3.Lerp(1.5f * Vector3.one, Vector3.one, songManager.PercentageOfSection);
        spriteRenderer.color = color;
    }

}
