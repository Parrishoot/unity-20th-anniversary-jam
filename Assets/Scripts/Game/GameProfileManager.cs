using System.Collections.Generic;
using UnityEngine;

public class GameProfileManager : MonoBehaviour
{
    [SerializeField]
    private List<GameProfile> profiles;

    private int profileIndex = 0;
    
    public GameProfile ActiveProfile
    {
        get
        {
            return profiles[profileIndex];
        }
    }

    public void Cycle()
    {
        profileIndex = (profileIndex + 1) % profiles.Count;
    }
}
