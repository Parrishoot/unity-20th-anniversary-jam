using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameGrid
{
    public GridOccupierType[,] Grid { get; private set; }
    
    public static Vector2Int NOT_FOUND = -Vector2Int.one;

    public int GridSize
    {
        get
        {
            return Grid.GetLength(0);
        }
    }

    public GameGrid(int gridSize)
    {
        ResetGrid(gridSize);
    }

    private void ResetGrid(int gridSize)
    {
        Grid = new GridOccupierType[gridSize, gridSize];

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Grid[i, j] = GridOccupierType.EMPTY;
            }
        }
    }

    public Vector2Int GetRandomCell()
    {
        return new Vector2Int(UnityEngine.Random.Range(0, GridSize), UnityEngine.Random.Range(0, GridSize));
    }

    public void MovePlayer(Direction direction)
    {
        Vector2Int currentCell = GetPlayerCell();
        Vector2Int targetCell = FindNextMoveableCell(currentCell, direction);

        Grid[currentCell.x, currentCell.y] = GridOccupierType.EMPTY;
        Grid[targetCell.x, targetCell.y] = GridOccupierType.PLAYER;
    }

    public Vector2Int FindNextMoveableCell(Vector2Int origin, Direction direction)
    {
        Vector2Int target = origin;
        Vector2Int directionVector = direction.Vector();

        while (CellIsMoveable(target + directionVector))
        {
            target += directionVector;
        }

        return target;

    }
    
    public bool IsValidGoalLocation(Vector2Int origin)
    {
        foreach (Vector2Int direction in DirectionUtil.AllVectors())
        {
            if (!CellIsMoveable(origin + direction))
            {
                return true;
            }
        }

        return false;
    }

    public bool CellIsOpen(Vector2Int cell)
    {
        if (!CellInBounds(cell))
        {
            return false;
        }

        return GridOccupierType.EMPTY.Equals(Grid[cell.x, cell.y]);
    }

    public bool CellIsMoveable(Vector2Int cell)
    {
        if (!CellInBounds(cell))
        {
            return false;
        }

        return GridOccupierType.EMPTY.Equals(Grid[cell.x, cell.y]) ||
            GridOccupierType.GOAL.Equals(Grid[cell.x, cell.y]); 
    }

    public bool CellInBounds(Vector2Int cell)
    {
        return cell.x >= 0 && cell.x < GridSize && cell.y >= 0 && cell.y < GridSize;
    }

    public Vector2Int GetPlayerCell()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (GridOccupierType.PLAYER.Equals(Grid[i, j]))
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        // Should never happen
        return NOT_FOUND;
    }

    public Vector2Int GetGoalCell()
    {
        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                if (GridOccupierType.GOAL.Equals(Grid[i, j]))
                {
                    return new Vector2Int(i, j);
                }
            }
        }

        return NOT_FOUND;
    }

    public void SetCell(Vector2Int cell, GridOccupierType occupierType)
    {
        Grid[cell.x, cell.y] = occupierType;
    }

    public Dictionary<Vector2Int, List<GridEdge>> GetEdges()
    {
        Dictionary<Vector2Int, List<GridEdge>> edges = new Dictionary<Vector2Int, List<GridEdge>>();

        for (int i = 0; i < Grid.GetLength(0); i++)
        {
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                Vector2Int cell = new Vector2Int(i, j);
                edges[cell] = new List<GridEdge>();

                if (!CellIsOpen(cell))
                {
                    continue;
                }

                foreach (Direction direction in DirectionUtil.All())
                {
                    GridEdge edge = new GridEdge(cell, FindNextMoveableCell(cell, direction));

                    if (edge.IsValid)
                    {
                        edges[cell].Add(edge);
                    }
                }
            }
        }
        
        return edges;
    }

}
