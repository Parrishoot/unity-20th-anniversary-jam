using System;
using DG.Tweening;
using UnityEngine;

public class HowToPlayPanelController : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = 2f;

    [SerializeField]
    private float transitonElasticity = 1f;

    [SerializeField]
    private RectTransform rectTransform;

    private bool transitioning = false;

    private float offscreenPosition;

    private bool showing = false;

    void Start()
    {
        offscreenPosition = rectTransform.anchoredPosition.y;
    }

    public void Toggle()
    {
        if (transitioning)
        {
            return;
        }

        showing = !showing;

        MoveToPosition(showing ? 0f : offscreenPosition, showing ? Ease.OutBack : Ease.InBack);
    }

    private void MoveToPosition(float position, Ease easeType)
    {
        transitioning = true;
        rectTransform.DOAnchorPosY(position, transitionTime)
            .SetEase(easeType, transitonElasticity)
            .OnComplete(() => transitioning = false);
    }

}
