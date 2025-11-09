using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text gameOverText;

    [SerializeField]
    private TMP_Text rankText;

    [SerializeField]
    private TMP_Text averageSpeedText;

    [SerializeField]
    private TMP_Text livesRemainingText;

    [SerializeField]
    private RectTransform rectTransform;

    [SerializeField]
    private float fallTime = 1f;

    [SerializeField]
    private float fallElasticity = 2f;

    public void ShowScreen(string rank, float averageSpeed, int livesRemaining)
    {
        gameOverText.text = rank == "F" ? "Game Over" : "You Win!";

        rankText.text = rank;
        averageSpeedText.text = averageSpeed.ToString("F2");
        livesRemainingText.text = livesRemaining.ToString();

        rectTransform.DOAnchorPosY(0f, fallTime).SetEase(Ease.OutBack, fallElasticity);
    }
}
