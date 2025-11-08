using TMPro;
using UnityEngine;

public class CountdownTextController : MonoBehaviour
{
    [SerializeField]
    private RoundManager roundManager;

    [SerializeField]
    private Color startColor = Color.white;

    [SerializeField]
    private Color endColor = Color.red;

    [SerializeField]
    private TMP_Text countdownText;

    private void Update()
    {
        countdownText.color = Color.Lerp(startColor, endColor, roundManager.CountdownTimer.ElaspedPercentage);
        countdownText.text = roundManager.CountdownTimer.RemainingTime.ToString("F2");
    }
}
