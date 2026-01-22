using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public bool activateNewMode = true;
    public GameObject wallPrefab;
    public GameObject wallPointPrefab;
    public Sculptor sculptor;
    public SoundHandler soundHandler;
    
    private Tile firstPoint = null;
    public WallPointScript debugFirstPoint = null;
    public GridManager gridManager;
    private float squareRootof2 =(float)Math.Sqrt(2f) ;
    public float maxWallSize = 1;
    public DataHolder dataHolder;
    


    private void Start()
    {
        sculptor = FindAnyObjectByType<Sculptor>();
        soundHandler = FindAnyObjectByType<SoundHandler>();
        dataHolder = FindAnyObjectByType<DataHolder>();
    }
    void Update()
    {
        if (activateNewMode)
        {
            
            DebugClicToWall(); 
        }
        
    }
    private void DebugClicToWall()
    {

        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits =Physics.RaycastAll(ray);
        Debug.DrawRay(ray.origin,ray.direction*300f,Color.magenta,200f);
        
        if (hits.Length<1) return;
        List<RaycastHit> hitsList = hits.ToList();

        hitsList.RemoveAll(x => !(x.collider.gameObject.CompareTag("WallPoint")));
        if (hitsList.Count<1) return;
        hitsList = hitsList.OrderBy(
            x => Vector3.Distance(new Vector3(ray.origin.x,x.transform.position.y,ray.origin.z),x.transform.position)
        ).ToList();
        
        if( !hitsList.First().collider.gameObject.TryGetComponent(out WallPointScript hitWallPoint ))return;
        
        
        if (debugFirstPoint is null)
        {
            debugFirstPoint = hitWallPoint;
            return;
        }

        

        
        if (debugFirstPoint.walls.Count>1||hitWallPoint.walls.Count>1)
        {
            
            debugFirstPoint = null;
            return;
        }
        

       DebugCreateWall(hitWallPoint,debugFirstPoint);
        debugFirstPoint = null;
    }

   

    public void CreateWall(Tile one, Tile two)
    {
       
        if (one is null || two is null||two==one)
        {
            return;
        }

        if (one.currentWallPointScript is null)
        {
            CreateWallPoint(one);
        }

        if (two.currentWallPointScript is null)
        {
            CreateWallPoint(two);
        }
        
        GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        WallScript wallScript = wall.GetComponent<WallScript>();
        wallScript.Create(one.currentWallPointScript,two.currentWallPointScript,gridManager,sculptor,soundHandler,dataHolder);
        
        firstPoint = null;
    }

    private void DebugCreateWall(WallPointScript one, WallPointScript two)
    {
        GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        WallScript wallScript = wall.GetComponent<WallScript>();
        wallScript.Create(one,two,gridManager,sculptor,soundHandler,dataHolder); 
    }
    
    
    

    

    public WallPointScript CreateWallPoint(Tile tile)
    {
        
        GameObject newWallPoint= Instantiate(wallPointPrefab, tile.transform.position, Quaternion.identity);
        WallPointScript toReturn = newWallPoint.GetComponent<WallPointScript>();
        toReturn.Create(soundHandler,dataHolder,tile);
        return toReturn;
    }
    
    public WallPointScript ExtendWallPoint(Vector3 origine)
    {
        GameObject newWallPoint= Instantiate(wallPointPrefab, origine, Quaternion.identity);
        WallPointScript toReturn = newWallPoint.GetComponent<WallPointScript>();
        toReturn.Create(soundHandler,dataHolder);
        return toReturn;
    }

    public void ExtendWall(WallPointScript one ,WallPointScript two)
    {
        GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        WallScript wallScript = wall.GetComponent<WallScript>();
        wallScript.Create(one,two,gridManager,sculptor,soundHandler,dataHolder);
    }
    
    
    
}
