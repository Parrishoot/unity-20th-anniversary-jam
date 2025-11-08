using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridRandomizer
{
    private GridController gridController;

    private List<Direction[]> corners = new List<Direction[]>()
    {
        new Direction[]{ Direction.RIGHT, Direction.UP },
        new Direction[]{ Direction.LEFT, Direction.UP },
        new Direction[]{ Direction.RIGHT, Direction.DOWN },
        new Direction[]{ Direction.LEFT, Direction.DOWN },
    };

    public GridRandomizer(GridController gridController)
    {
        this.gridController = gridController;
    }


    public GameGrid GetRandomGrid(int gridSize, int minCorners = 3, int maxCorners = 5)
    {
        GameGrid grid = null;
        
        for(int i = 0; i < 10; i++)
        {
            grid = TryRandomizeGrid(gridSize, minCorners, maxCorners);
            
            if(grid != null)
            {
                break;
            }
        }

        return grid;
    }

    private GameGrid TryRandomizeGrid(int gridSize, int minCorners, int maxCorners)
    {
        GameGrid grid = new GameGrid(gridSize);
        int numBlocks = UnityEngine.Random.Range(minCorners, maxCorners + 1);

        // Spawn Blocks
        while (numBlocks > 0)
        {
            Vector2Int cell = grid.GetRandomCell();
            Direction[] cornerType = corners[UnityEngine.Random.Range(0, corners.Count)];

            if (IsValidCornerLocation(grid, cell, cornerType))
            {
                foreach (Direction direction in cornerType)
                {
                    if(grid.CellInBounds(cell + direction.Vector()))
                    {
                        grid.SetCell(cell + direction.Vector(), GridOccupierType.BLOCK);   
                    }
                }
                
                numBlocks--;
            }
        }

        Dictionary<Vector2Int, List<GridEdge>> edges = grid.GetEdges();
        List<Vector2Int> path = new List<Vector2Int>();

        Vector2Int playerCell = Vector2Int.zero;
        Vector2Int goalCell = Vector2Int.zero;

        int attempts = 100;

        do
        {
            do
            {
                playerCell = grid.GetRandomCell();
            }
            while (!grid.CellIsOpen(playerCell));

            do
            {
                goalCell = grid.GetRandomCell();
            }
            while (!grid.CellIsOpen(goalCell) || goalCell.Equals(playerCell));

            path = new PathFinder(grid.Grid, edges, playerCell, goalCell).FindPath();
            attempts--;
        }
        while (path.Count < 4 && attempts > 0);

        grid.SetCell(playerCell, GridOccupierType.PLAYER);
        grid.SetCell(goalCell, GridOccupierType.GOAL);

        if (attempts <= 0)
        {
            Debug.LogWarning("Impossible!");
            return null;
        }

        return grid;
    }

    private bool IsValidCornerLocation(GameGrid grid, Vector2Int target, Direction[] corner)
    {
        List<Vector2Int> cornerCells = corner.Select(x => x.Vector() + target).ToList();

        if (!grid.CellIsOpen(cornerCells.First()) && !grid.CellIsOpen(cornerCells.Last()))
        {
            return false;
        }

        return true;
    }
}
