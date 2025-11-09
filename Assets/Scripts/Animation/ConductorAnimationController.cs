using UnityEngine;

public class ConductorAnimationController : MonoBehaviour
{
    [SerializeField]
    private float transitionSpeed = 1f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private PlayerMovementController playerMovementController;

    [SerializeField]
    private float wiggleAmount = .25f;

    [SerializeField]
    private float wiggleSpeed = .25f;

    private Vector2 currentAnimation = Vector2.zero;

    private Vector2 targetAnimation = Vector2.zero;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMovementController.MovementProcessed += (x) => targetAnimation = x;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 frameTarget = targetAnimation * ((1 - wiggleAmount) + (Mathf.Sin(Time.time * wiggleSpeed) + 1) / 2 * wiggleAmount);
        currentAnimation = Vector2.Lerp(currentAnimation, frameTarget, transitionSpeed * Time.deltaTime);

        animator.SetFloat("Horizontal", currentAnimation.x);
        animator.SetFloat("Vertical", currentAnimation.y);
    }
}
