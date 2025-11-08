using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [field:SerializeReference]
    public int GridSize { get; private set; } = 5;

    [Header("Randomize Props")]
    [field:SerializeReference]
    public int MinBlocks { get; private set; } = 3;

    [field:SerializeReference]
    public int MaxBlocks { get; private set; } = 5;

    [field:SerializeReference]
    public int MinMoves { get; private set; } = 3;

    [field:SerializeReference]
    public int MaxMoves { get; private set; } = 5;

    [Header("Helpers")]
    [SerializeField]
    private GridSpawner gridSpawner;

    public GameGrid Grid
    {
        get
        {
            return gameGrid;
        }
    }

    private GameGrid gameGrid;

    private GridRandomizer gridRandomizer;

    public Action<Vector2Int> PlayerPositionReset { get; set; }


    void Start()
    {
        gridRandomizer = new GridRandomizer(this);
        RandomizeGrid();   
    }

    [ProButton]
    public void RandomizeGrid()
    {
        ResetGrid();
    }

    private void ResetGrid()
    {
        gridSpawner.ClearPrefabs();
        gameGrid = gridRandomizer.GetRandomGrid(GridSize, MinBlocks, MaxBlocks);

        PlayerPositionReset?.Invoke(gameGrid.GetPlayerCell());
        gridSpawner.SpawnGrid(gameGrid.Grid);
    }

    public void MovePlayer(Direction direction)
    {
        Vector2Int currentCell = gameGrid.GetPlayerCell();
        Vector2Int targetCell = gameGrid.FindNextMoveableCell(currentCell, direction);

        gameGrid.SetCell(currentCell, GridOccupierType.EMPTY);
        gameGrid.SetCell(targetCell, GridOccupierType.PLAYER);
    }

    public Vector2 GetPositionForCell(Vector2Int cell)
    {
        return gridSpawner.GetPositionForCell(cell);
    }

    
    public bool IsWinner()
    {
        // If there's no more goal, that's a winner!
        return gameGrid.GetGoalCell() == GameGrid.NOT_FOUND;
    }
}
