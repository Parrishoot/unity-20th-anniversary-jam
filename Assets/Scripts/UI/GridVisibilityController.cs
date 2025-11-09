using UnityEngine;

public class GridVisibilityController : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gridSpriteRenderer;

    void Start()
    {
        HideGrid();
    }

    public void ShowGrid()
    {
        gridSpriteRenderer.enabled = true;

        foreach (SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = true;
        }
    }
    
    public void HideGrid()
    {
        gridSpriteRenderer.enabled = false;

        foreach(SpriteRenderer renderer in GetComponentsInChildren<SpriteRenderer>())
        {
            renderer.enabled = false;
        }
    }
}
