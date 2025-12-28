using System;
using System.Collections.Generic;
using System.Linq;
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
    private GameObject camObject;
    private Camera camScript;
    public int voidSize = 5;

    private GameObject playSpace;
    public GameObject theVoid;

    public Vector3 gridCenter;
    private DataHolder dataHolder;

    public Vector2 screensize;
    public Vector3 testVec;

    private void Start()
    {
        dataHolder = FindAnyObjectByType<DataHolder>();
        camObject = GameObject.FindGameObjectWithTag("MainCamera");
        camScript = camObject.GetComponent<Camera>();

        playSpace = GameObject.FindGameObjectWithTag("PlaySpace");
        theVoid = GameObject.FindGameObjectWithTag("Void");
        CreateGrid();
        PositionCam();
        PositionPlanes();
        
    }

    private void CreateGrid()
    {
        grid = new Tile[gridSize.x, gridSize.y];
        for (var x = 0; x < gridSize.x; x++)
        {
            for (var y = 0; y < gridSize.y; y++)
            {
                Vector2Int coords = new Vector2Int(x, y);
                GameObject tileGO = Instantiate(tilePrefab, GridToWorldPos(coords), Quaternion.identity);
                tileGO.transform.SetParent(transform);
                tileGO.GetComponent<Tile>().Initialize(coords, this,dataHolder);
                allAnchor.Add(tileGO);
                
            }
        }   
        allAnchor.ForEach(x=>allTile.Add(x.GetComponent<Tile>()));
        CreateStartWall();
    }

    private void PositionCam()
    {
        Debug.Assert(grid is not null);
        gridCenter= (allAnchor.Last().transform.position-allAnchor.First().transform.position)*0.5f;
        camObject.transform.position =gridCenter+(Vector3.up*gridCenter.magnitude );
        
        camScript.orthographicSize =  gridSize.magnitude;
    }

    private void PositionPlanes()
    {
        playSpace.transform.position = new Vector3(gridCenter.x,-0.5f,gridCenter.z);
        playSpace.transform.localScale = new Vector3(gridSize.x/5.15f,1,gridSize.y/5.15f);
        
        screensize = new Vector2(Screen.width,Screen.height);
        
        float planeHeighScale = 2f*camScript.orthographicSize/10f;
        float planeWidthScale = planeHeighScale*camScript.aspect;
        testVec = new Vector3(planeWidthScale, 0, planeHeighScale);
        
        theVoid.transform.position = new Vector3(gridCenter.x,-0.75f,gridCenter.z);
        theVoid.transform.localScale = new Vector3(planeWidthScale,1,planeHeighScale);

        
    }

    private void Update()
    {
        if (screensize.x != Screen.width || screensize.y != Screen.height)
        {
            PositionPlanes();
        }
    }
    private void CreateStartWall()
    { 
        List<Tile> allUpBorder = allTile.FindAll(x => x.coords.y.Equals(gridSize.y-1));
       allUpBorder.ForEach(x=>wallCreator.CreateWall(x,allUpBorder.Find(a=>a.coords.x.Equals(x.coords.x+1))));
       List<Tile> allDownBorder = allTile.FindAll(x => x.coords.y.Equals(0));
       allDownBorder.ForEach(x=>wallCreator.CreateWall(x,allDownBorder.Find(a=>a.coords.x.Equals(x.coords.x+1))));
       
       List<Tile> allRightBorder = allTile.FindAll(x => x.coords.x.Equals(gridSize.x-1));
       allRightBorder.ForEach(x=>wallCreator.CreateWall(x,allRightBorder.Find(a=>a.coords.y.Equals(x.coords.y+1))));
       List<Tile> allLeftBorder = allTile.FindAll(x => x.coords.x.Equals(0));
       allLeftBorder.ForEach(x=>wallCreator.CreateWall(x,allLeftBorder.Find(a=>a.coords.y.Equals(x.coords.y+1))));

        
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