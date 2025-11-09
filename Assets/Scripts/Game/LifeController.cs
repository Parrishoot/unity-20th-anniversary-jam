using com.cyborgAssets.inspectorButtonPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = 1;

    [SerializeField]
    private float rotateAmount = 5;

    [SerializeField]
    private float fallElasticity = 2f;

    [SerializeField]
    private float fallAmount = 3f;

    [SerializeField]
    private float fallSpeed = 1f;

    [SerializeField]
    private Image image;

    void Update()
    {
        transform.eulerAngles = Vector3.forward * Mathf.Sin(Time.time * rotateSpeed) * rotateAmount;
    }

    [ProButton]
    public void Despawn()
    {
        DOTween.Sequence()
            .Append(transform.DOLocalMoveY(transform.localPosition.y - fallAmount, fallSpeed).SetEase(Ease.InBack, fallElasticity))
            .Join(image.DOFade(0f, fallSpeed))
            .OnComplete(() => Destroy(gameObject));
    }
}
