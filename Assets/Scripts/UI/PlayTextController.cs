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
            playText.SetText($"PLAY");
            return;
        }
        
        playText.SetText($"PLAY ({gameProfileManager.ActiveProfile.Label.ToUpper()})");
    }
}
