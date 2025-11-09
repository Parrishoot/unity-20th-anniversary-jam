using TMPro;
using UnityEngine;

public class PlayTextController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playText;

    void Update()
    {
        GameProfileManager gameProfileManager = FindAnyObjectByType<GameProfileManager>();
        if (FindAnyObjectByType<GameProfileManager>() == null)
        {
            playText.SetText($"Play");
            return;
        }
        
        playText.SetText($"Play ({gameProfileManager.ActiveProfile.Label})");
    }
}
