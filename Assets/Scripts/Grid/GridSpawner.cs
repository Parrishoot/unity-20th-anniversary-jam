using System.Collections.Generic;
using com.cyborgAssets.inspectorButtonPro;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    [Header("Grid Info")]
    [SerializeField]
    private float pixelsPerUnit = 8;

    [SerializeField]
    private float borderPixels = 1;

    [Header("Spawn Object Info")]
    [SerializeField]
    private Transform gridBlockTransform;

    [SerializeField]
    private GameObject gridBlockPrefab;

    [SerializeField]
    private GameObject gridGoalPrefab;

    private List<GameObject> spawnedPrefabs = new List<GameObject>();

    private float SpacePerPixel
    {
        get
        {
            return 1 / pixelsPerUnit;
        }
    }

    private float PixelCellSize
    {
        get
        {
            return pixelsPerUnit - 2 * borderPixels;
        }
    }

    public Vector2 GetPositionForCell(Vector2Int cell)
    {
        return new Vector2(GetOffsetForCellCoordinateInWorldSpace(cell.x),
            GetOffsetForCellCoordinateInWorldSpace(cell.y));
    }

    private float GetOffsetForCellCoordinateInPixels(float coordinate)
    {
        return ((1 + coordinate) * borderPixels) + // Border Offset
            (coordinate * PixelCellSize) + // Offset for cell
            PixelCellSize / 2; // Account for center-adjusted block
    }

    private float GetOffsetForCellCoordinateInWorldSpace(float coordinate)
    {
        return GetOffsetForCellCoordinateInPixels(coordinate) * SpacePerPixel;
    }

    [ProButton]
    public void SpawnBlockAtCell(Vector2Int cell)
    {
        SpawnPrefabAtCell(cell, gridBlockPrefab);
    }

    [ProButton]
    public void SpawnGoalAtCell(Vector2Int cell)
    {
        SpawnPrefabAtCell(cell, gridGoalPrefab);
    }

    [ProButton]
    public void SpawnPlayerAtCell(Vector2Int cell)
    {
        SpawnPrefabAtCell(cell, gridGoalPrefab);
    }

    public void SpawnGrid(GridOccupierType[,] grid)
    {
        for(int i = 0; i < grid.GetLength(0); i++) {
            for(int j = 0; j < grid.GetLength(1); j++) {
                SpawnOccupierAtCell(new Vector2Int(i, j), grid[i, j]);
            }   
        }
    }

    public void SpawnOccupierAtCell(Vector2Int cell, GridOccupierType occupierType)
    {
        switch (occupierType)
        {
            case GridOccupierType.GOAL:
                SpawnGoalAtCell(cell);
                break;

            case GridOccupierType.BLOCK:
                SpawnBlockAtCell(cell);
                break;

        }
    }

    [ProButton]
    public void ClearPrefabs()
    {
        foreach (GameObject prefab in spawnedPrefabs)
        {
            if (Application.isEditor)
            {
                DestroyImmediate(prefab);
            }
            else
            {
                Destroy(prefab);
            }
        }

        spawnedPrefabs = new List<GameObject>();
    } 

    private void SpawnPrefabAtCell(Vector2Int cell, GameObject prefab)
    {
        GameObject gridObject = Instantiate(prefab, gridBlockTransform);
        gridObject.transform.localPosition = GetPositionForCell(cell);

        spawnedPrefabs.Add(gridObject);
    }
}
