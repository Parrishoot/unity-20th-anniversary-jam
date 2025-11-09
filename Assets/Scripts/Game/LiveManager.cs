using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LiveManager : MonoBehaviour
{
    [SerializeField]
    private RoundManager roundManager;

    [SerializeField]
    private GameObject lifeControllerPrefab;

    [SerializeField]
    private Transform lifeSpawnTransform;

    [field: SerializeReference]
    public int NumLives { get; private set; }
    
    public Action RanOutOfLives { get; set; }

    private Stack<LifeController> lifeStack = new Stack<LifeController>();

    public int LivesRemaning
    {
        get
        {
            return lifeStack.Count;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < NumLives; i++)
        {
            LifeController controller = Instantiate(lifeControllerPrefab, lifeSpawnTransform).GetComponent<LifeController>();
            lifeStack.Push(controller);
        }

        roundManager.RoundEnded += CheckLives;
    }

    private void CheckLives(RoundState state)
    {
        if (state != RoundState.FAILED || lifeStack.Count == 0)
        {
            return;
        }

        LifeController controller = lifeStack.Pop();
        controller.Despawn();

        if(LivesRemaning == 0)
        {
            RanOutOfLives?.Invoke();
        }
    }
}
