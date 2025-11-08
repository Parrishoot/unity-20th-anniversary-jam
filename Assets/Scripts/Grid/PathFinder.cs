using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;

/// <summary>
/// We love A* :) https://en.wikipedia.org/wiki/A*_search_algorithm
/// </summary>
public class PathFinder
{
    private GridOccupierType[,] grid;

    private Vector2Int startingCell;

    private Vector2Int targetCell;

    private Dictionary<Vector2Int, List<GridEdge>> edges;

    public PathFinder(GridOccupierType[,] grid, Dictionary<Vector2Int, List<GridEdge>> edges, Vector2Int startingCell, Vector2Int targetCell)
    {
        this.grid = grid;
        this.edges = edges;
        this.startingCell = startingCell;
        this.targetCell = targetCell;
    }


    public List<Vector2Int> FindPath()
    {
        List<Vector2Int> path = new List<Vector2Int>();

        List<Vector2Int> openSet = new List<Vector2Int>
        {
            startingCell
        };

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        Dictionary<Vector2Int, int> gScore = new Dictionary<Vector2Int, int>();
        gScore[startingCell] = 0;

        while (openSet.Count > 0)
        {
            Vector2Int current = GetNext(openSet, gScore);

            if (current == targetCell)
            {
                return ReconstructPath(cameFrom, current);
            }

            foreach (GridEdge gridEdge in edges[current])
            {
                Vector2Int neighbor = gridEdge.End;
                int tentativeGScore = gScore[current] + 1;

                if (!gScore.ContainsKey(neighbor) || tentativeGScore < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;

                    if (!openSet.Contains(neighbor))
                    {
                        openSet.Add(neighbor);
                    }
                }
            }
        }

        return path;
    }

    private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int target)
    {
        Vector2Int current = target;
        List<Vector2Int> path = new List<Vector2Int>
        {
            target
        };

        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }

        return path;
    }

    private Vector2Int GetNext(List<Vector2Int> openSet, Dictionary<Vector2Int, int> gScore)
    {
        Vector2Int next = openSet.OrderBy(x => gScore[x]).First();
        openSet.Remove(next);

        return next;
    }
}
