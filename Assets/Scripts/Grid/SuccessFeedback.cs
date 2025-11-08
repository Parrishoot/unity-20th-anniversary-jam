using DG.Tweening;
using UnityEngine;

public class SuccessFeedback : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float fadeSpeed = 1f;

    [SerializeField]
    private float rotateAmount = 2f;

    [SerializeField]
    private float finalScale = 2;

    private void Start()
    {
        DOTween.Sequence()
            .Append(transform.DOScale(finalScale, fadeSpeed).SetEase(Ease.OutSine))
            .Join(spriteRenderer.DOFade(0f, fadeSpeed))
            .Join(transform.DOLocalRotate(Vector3.forward * Random.Range(-rotateAmount, rotateAmount), fadeSpeed).SetEase(Ease.OutSine))
            .OnComplete(() => Destroy(this));
    }
}
