using UnityEngine;


[CreateAssetMenu(fileName = "GameProfile", menuName = "Game Profiles", order = 0)]
public class GameProfile : ScriptableObject {

    [field: SerializeReference]
    public string Label { get; private set; }

    [field: SerializeReference]
    public AudioClip Song { get; private set; }

    [field: SerializeReference]
    public float MinBPM { get; private set; } = 120f;

    [field: SerializeReference]
    public float MaxBPM { get; private set; } = 140f;

    [field: SerializeReference]
    public float BeatsPerPuzzle { get; private set; } = 8;

}

