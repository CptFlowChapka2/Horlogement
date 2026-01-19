using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
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
    private SoundHandler soundHandler;
    private DataHolder dataHolder;
   

    public bool isSelected = false;



   

    private void Start()
    {
        sculptor = FindAnyObjectByType<Sculptor>();
        gridManager = FindAnyObjectByType<GridManager>();
        wallCreator = FindAnyObjectByType<WallCreator>();
        mouse = InputSystem.actions.FindAction("Look");
        thisCollider = GetComponent<SphereCollider>();
        
        //InvokeRepeating(nameof(CheckWalls),0,0.2f);
    }
    public void Create(SoundHandler soundHandlar,DataHolder datoHolder,Tile tile=null)
    {
        dataHolder = datoHolder;
        if (tile is not null)
        {
            linkedTile = tile;
            tile.currentWallPointScript = this;
        
            gameObject.transform.position = linkedTile.transform.position;
        }
        soundHandler = soundHandlar;
        
        soundHandlar.CreateAudioSource(gameObject);
    }

    private void Update()
    {
        CheckWalls();
    }


    public void EndMouvement()
    {

        if (ProximityCheck(out var results)) return;

        if (results.currentWallPointScript is null)
        {


            if (linkedTile is not null)
            {
                linkedTile.currentWallPointScript = null;
            }
            
            results.currentWallPointScript = this;
            linkedTile = results;
            transform.position = results.transform.position;
            walls.ForEach(x=>x.Moove());
           List<WallScript> toBreak = walls.FindAll(x => x.length > maxMouvment * 1.1f);
           foreach (var el in toBreak)
           {
               WallPointScript one = el.one;   
               WallPointScript two = el.two;   
               el.Break();
               one.CheckWalls();
               two.CheckWalls();
           }
           


        }
        else if(results.currentWallPointScript)
        {
            walls.FindAll(x => x!=null)
                .ForEach(x => x.Create(x.one, results.currentWallPointScript, gridManager,sculptor,soundHandler,dataHolder));
            walls.ForEach(x=>x.Moove());
            walls.Clear();
            Destroy(gameObject);
        }
        else
        {
            
            foreach (var el in walls)
            {
                WallPointScript one = el.one;   
                WallPointScript two = el.two;   
                el.Break();
                one.CheckWalls();
                two.CheckWalls();
            }
        }
        soundHandler.Moove(gameObject);
       CheckWalls();
        walls.ForEach(x=>x.ToggleColision(false));

        results = null;
    }
    private bool ProximityCheck(out Tile results)
    {
        var resultsList =gridManager.allTile.OrderBy(
            x => Vector3.Distance(this.transform.position,x.transform.position)
        ).ToArray();

        if ((Vector3.Distance(this.transform.position, resultsList.First().transform.position) > anchorCheckRange))
        {
            
            Destroy(gameObject);
            results = null;
            return true;
        }

        results = resultsList.First();
        return false;
    }

    private void OnDestroy()
    {
        if(soundHandler == null||gameObject == null) return;

        if (gameObject != null)
        {
            soundHandler.Kill(null,gameObject);
        }
        
        walls.ForEach(x=>x.Break());
        if (linkedTile != null) 
        {
            linkedTile.currentWallPointScript = null;
        }
        
    }

    public void CheckWalls()
    {
        if (walls.TrueForAll(x => x == null))
        {
            walls.Clear();
            Destroy(gameObject);
        }
    }
}