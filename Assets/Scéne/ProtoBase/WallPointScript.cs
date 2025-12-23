using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class WallPointScript : MonoBehaviour
{

    private InputAction mouse;
    public Tile linkedTile;
    
    [SerializeField] private float anchorCheckRange = 2f;
    public float maxMouvment = 2f;
    private WallCreator wallCreator;
    private SphereCollider thisCollider;
    public GridManager gridManager;
    public List<WallScript> walls;
    
    private float squareRootof2 =(float)Math.Sqrt(2f) ;

    private Sculptor sculptor;

    private Tile foundTile;

    public bool isSelected = false;



    private void Update()
    {
        if( walls.TrueForAll(x => x is null)) DestroyImmediate(gameObject);
    }

    private void Start()
    {
        sculptor = FindAnyObjectByType<Sculptor>();
        gridManager = FindAnyObjectByType<GridManager>();
        maxMouvment = (gridManager.tileSize.magnitude*1f) *squareRootof2;
        wallCreator = FindAnyObjectByType<WallCreator>();
        mouse = InputSystem.actions.FindAction("Look");
        thisCollider = GetComponent<SphereCollider>();
    }
    public void Create(Tile tile)
    {
        linkedTile = tile;
        tile.currentWallPointScript = this;
        gameObject.transform.position = linkedTile.transform.position;
    }

    

    
    public void EndMouvement()
    {

        if (ProximityCheck(out var results)) return;


        if (!results.First().TryGetComponent(out  foundTile)) return;

        if (foundTile.currentWallPointScript is null)
        {
           
            
                linkedTile.currentWallPointScript = null;
            
            foundTile.currentWallPointScript = this;
            linkedTile = foundTile;
            transform.position = foundTile.transform.position;
            walls.ForEach(x=>x.Moove());
            walls.FindAll(x => x.length > (gridManager.tileSize.x * 1.2f) * squareRootof2).ForEach(x=>x.Break());


        }
        else if (linkedTile.currentWallPointScript is  null||linkedTile.currentWallPointScript.Equals(this))
        {
            Debug.Log("hi");
            transform.position = linkedTile.transform.position;
            walls.ForEach(x=>x.Moove());
            walls.FindAll(x => x.length > (gridManager.tileSize.x * 1.2f) * squareRootof2).ForEach(x=>x.Break());
        }
        else
        {
            walls.FindAll(x=>x!=null).ForEach(x=>x.MergeWalls(foundTile.currentWallPointScript,this,gridManager));

            Destroy(gameObject);
        }

        foundTile = null;
    }
    private bool ProximityCheck(out Collider[] results)
    {

        results = new Collider[8];
        Physics.OverlapSphereNonAlloc(transform.position, anchorCheckRange,results, Physics.AllLayers, QueryTriggerInteraction.Collide);


        var resultsList = results.ToList();

        

        resultsList.RemoveAll(x => x is null||!x.gameObject.CompareTag("Anchor"));
        if (resultsList.Count.Equals(0))
        {
            
            transform.position = linkedTile.transform.position;
            walls.ForEach(x=>x.Moove());
            return true;
        }
        results = resultsList.ToArray();
        
        return false;
    }

    private void OnDestroy()
    {
        sculptor.currentSelection = null;
    }
}