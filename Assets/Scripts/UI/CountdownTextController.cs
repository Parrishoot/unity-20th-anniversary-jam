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
        if(roundManager.FailCountdownTimer != null)
        {
            countdownText.color = Color.Lerp(startColor, endColor, roundManager.FailCountdownTimer.ElaspedPercentage);
            countdownText.text = roundManager.FailCountdownTimer.RemainingTime.ToString("F2");   
        }
    }
}
