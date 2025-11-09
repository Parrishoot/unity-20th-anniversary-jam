using UnityEngine;

public class ToggleGameModeController : MonoBehaviour
{
    public void Toggle()
    {
        FindAnyObjectByType<GameProfileManager>()?.Cycle();
    }
}
