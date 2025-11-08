using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class DirectionUtil
{
    public static Direction RandomDirection()
    {
        return (Direction) UnityEngine.Random.Range(0, 4);
    }

    public static Direction RandomDirection(List<Direction> options)
    {
        return options[UnityEngine.Random.Range(0, options.Count)];
    }

    public static List<Direction> All()
    {
        return Enum.GetValues(typeof(Direction)).Cast<Direction>().ToList();
    }

    public static List<Vector2Int> AllVectors()
    {
        return All().Select(x => x.Vector()).ToList();
    }

    public static Vector2Int Vector(this Direction direction)
    {
        return direction switch
        {
            Direction.LEFT => Vector2Int.left,
            Direction.RIGHT => Vector2Int.right,
            Direction.UP => Vector2Int.up,
            Direction.DOWN => Vector2Int.down,
            _ => throw new NotImplementedException(),
        };
    }
}
