using DG.Tweening;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    
    [SerializeField]
    private float height = .25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOLocalMoveY(transform.localPosition.y - height, speed)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
