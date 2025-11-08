using System;
using System.Collections.Generic;
using UnityEngine;

public class GridEdge
{
    public GridEdge(Vector2Int a, Vector2Int b)
    {
        Start = a;
        End = b;

        vertices = new HashSet<Vector2Int>();
        vertices.Add(Start);
        vertices.Add(End);
    }

    public Vector2Int Start { get; private set; }

    public Vector2Int End { get; private set; }

    private HashSet<Vector2Int> vertices;

    public bool IsValid
    {
        get
        {
            return Start != End;
        }
    }

    public HashSet<Vector2Int> Vertices
    {
        get
        {
            return vertices;
        }
    }

    // public override bool Equals(object obj)
    // {
    //     return Equals(obj as GridEdge);
    // }

    // public bool Equals(GridEdge other)
    // {
    //     return other != null && other.Vertices.SetEquals(vertices);
    // }

    // public override int GetHashCode()
    // {
    //     if (A.x > B.x || A.y > B.y)
    //     {
    //         return HashCode.Combine(A, B);
    //     }

    //     return HashCode.Combine(B, A);
    // }

    public override string ToString()
    {
        return $"({Start.x}, {Start.y}) -> ({End.x}, {End.y})";
    }
}
