using DG.Tweening;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    
    [SerializeField]
    private float rotateAmount = 10f;

    [SerializeField]
    private float height = .25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.eulerAngles = transform.eulerAngles + rotateAmount * Vector3.forward;

        transform.DOLocalMoveY(transform.localPosition.y - height, speed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        DOTween.Sequence()
            .Append(transform.DOLocalRotate(transform.localEulerAngles + Vector3.forward * -rotateAmount * 2, speed * 1.5f).SetEase(Ease.InOutSine))
            .Append(transform.DOLocalRotate(transform.localEulerAngles, speed * 1.5f).SetEase(Ease.InOutSine))
            .SetLoops(-1);
    }
}
