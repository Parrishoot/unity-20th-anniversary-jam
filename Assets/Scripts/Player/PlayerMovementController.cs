using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = .25f;

    [SerializeField]
    private float movementElasiticty = .25f;

    [SerializeField]
    private GridController gridController;

    private const float DISTANCE_THRESHOLD = .001f;

    private const int QUEUE_TOO_BIG = 3;

    private Queue<Vector2Int> MovementQueue = new Queue<Vector2Int>();

    private Tween currentTween;
    
    private bool speedingUp = false;

    void Awake()
    {
        gridController.PlayerPositionReset += ResetPosition;
    }

    private void ResetPosition(Vector2Int newCell)
    {
        MovementQueue.Clear();

        if(IsTweening())
        {
            currentTween.Complete();
            currentTween = null;
            speedingUp = false;
        }

        transform.localPosition = gridController.GetPositionForCell(newCell);
    }

    public void Move(Direction direction)
    {
        gridController.MovePlayer(direction);

        Vector2Int nextCell = gridController.Grid.GetPlayerCell();

        if (IsTweening())
        {
            MovementQueue.Enqueue(nextCell);
            return;
        }

        TweenToCell(nextCell);
    }
    
    private void CheckMoveQueue()
    {
        // Check for a winner
        if (gridController.IsWinner())
        {
            gridController.TriggerWin();
            return;
        }

        // Otherwise, check if there are any more moves
        // to process
        if (MovementQueue.Count <= 0)
        {
            currentTween = null;
            speedingUp = false;
            return;
        }

        Vector2Int nextCell = MovementQueue.Dequeue();
        TweenToCell(nextCell);
    }

    private void TweenToCell(Vector2Int cell)
    {
        Vector2 targetPosition = gridController.GetPositionForCell(cell);

        if(Vector2.Distance(transform.localPosition, targetPosition) <= DISTANCE_THRESHOLD)
        {
            CheckMoveQueue();
        }

        float tweenSpeed = movementSpeed;

        if(speedingUp || MovementQueue.Count > QUEUE_TOO_BIG)
        {
            tweenSpeed /= 2;
            speedingUp = true;
        }

        currentTween = currentTween = transform.DOLocalMove(targetPosition, tweenSpeed)
            .SetEase(Ease.InCubic, overshoot: movementElasiticty)
            .OnComplete(CheckMoveQueue);
    }
    
    private bool IsTweening()
    {
        return currentTween != null && currentTween.IsPlaying();
    }
}
