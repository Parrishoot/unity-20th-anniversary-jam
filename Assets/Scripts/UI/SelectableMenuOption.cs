using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SelectableMenuOption : MonoBehaviour
{
    [SerializeField]
    private Color highlightedColor;

    [SerializeField]
    private float highlightedScale = 1.2f;

    [SerializeField]
    private float highlightedTransitionTime = .5f;

    [SerializeField]
    private UnityEvent OnSelect;

    [SerializeField]
    private TMP_Text text;

    private bool highlighted = false;

    private Color unhighlightedColor;

    private float unhighlightedScale;

    private Color currentTargetColor;

    private float currentTargetScale;

    // Update is called once per frame
    void Awake()
    {
        unhighlightedColor = text.color;
        unhighlightedScale = transform.localScale.x;
    }

    public void BeginHighlight()
    {
        highlighted = true;
    }

    public void StopHighlight()
    {
        highlighted = false;
    }

    public void Select()
    {
        OnSelect?.Invoke();
    }

    public void Update()
    {
        text.color = Color.Lerp(text.color, highlighted ? highlightedColor : unhighlightedColor, highlightedTransitionTime * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, highlighted ? Vector3.one * highlightedScale : Vector3.one * unhighlightedScale, highlightedTransitionTime * Time.deltaTime);
    }
}
