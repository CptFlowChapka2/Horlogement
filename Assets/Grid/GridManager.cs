using System;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2Int gridSize = new Vector2Int(10, 10);
    public Vector2 tileSize = Vector2.one;
    public GameObject tilePrefab;
    public List<GameObject> allAnchor = new List<GameObject>();
    public List<Tile> allTile = new List<Tile>();
    private Tile[,] grid;
    public WallCreator wallCreator;

    private void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Tile[gridSize.x, gridSize.y];
        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                GameObject tileGO = Instantiate(tilePrefab, GridToWorldPos(coords), Quaternion.identity);
                tileGO.transform.SetParent(transform);
                tileGO.GetComponent<Tile>().Initialize(coords, this);
                allAnchor.Add(tileGO);
                
            }
        }
        allAnchor.ForEach(x=>allTile.Add(x.GetComponent<Tile>()));
        CreateStartWall();
    }


    private void CreateStartWall()
    {
        List<Tile> allUpBorder = allTile.FindAll(x => x.coords.y.Equals(gridSize.y));
        allUpBorder.ForEach(x=>wallCreator.CreateWall(
            x, allUpBorder.Find(a=>a.coords.x.Equals(x.coords.x+1)&&!a.isWall)));
        //todo fixe this

        
    }

    //Helpers : 
    public Vector2Int WorldToGridPos(Vector3 worldPos)
    {
        Vector2 planarPos = new Vector2(worldPos.x, worldPos.z);
        Vector2Int gridPos = Vector2Int.zero;
        gridPos.x = Mathf.FloorToInt((planarPos.x + tileSize.x / 2) / tileSize.x);
        gridPos.y = Mathf.FloorToInt((planarPos.y + tileSize.y / 2) / tileSize.y);
        return gridPos;
    }

    public Vector3 GridToWorldPos(Vector2Int gridPos)
    {
        return new Vector3(gridPos.x * tileSize.x, 0, gridPos.y * tileSize.y);
    }
    
    public Tile GetTileAt(int x, int y)
    {
        if (x < 0 || x >= gridSize.x || y < 0 || y >= gridSize.y)
        {
            Debug.LogWarning("Coordinates outside the range of the grid");
            return null;
        }

        return grid[x, y];
    }

    public Tile GetTileAt(Vector2Int coords)
    {
        if (coords.x < 0 || coords.x >= gridSize.x || coords.y < 0 || coords.y >= gridSize.y)
        {
            Debug.LogWarning("Coordinates outside the range of the grid");
            return null;
        }

        return grid[coords.x, coords.y];
    }
}