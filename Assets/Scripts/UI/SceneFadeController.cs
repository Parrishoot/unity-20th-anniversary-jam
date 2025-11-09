using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeController : MonoBehaviour
{
    [SerializeField]
    private float fadeTime = 2f;

    [SerializeField]
    private Image image;

    [SerializeField]
    private Transform lightTransform;

    private Vector3 targetRotation;

    public Action OnFadeIn { get; set; }

    public Action OnFadeOut { get; set; }

    public float FadeAmount
    {
        get { return image.color.a; }
    }

    public void Awake()
    {
        targetRotation = lightTransform.localEulerAngles;
        lightTransform.localEulerAngles += Vector3.right * 30f;
    }

    public void FadeIn()
    {
        DOTween.Sequence()
            .Append(image.DOFade(0f, fadeTime))
            .Join(lightTransform.DOLocalRotate(targetRotation, fadeTime).SetEase(Ease.InOutSine))
            .OnComplete(() => OnFadeIn?.Invoke());
    }

    public void FadeOut()
    {
        image.DOFade(1f, fadeTime / 2).OnComplete(() => OnFadeOut?.Invoke());
    }
}
