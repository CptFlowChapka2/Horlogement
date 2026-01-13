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
    public GridManager gridManager;
    private float squareRootof2 =(float)Math.Sqrt(2f) ;
    public float maxWallSize = 1;
    


    private void Start()
    {
        sculptor = FindAnyObjectByType<Sculptor>();
        soundHandler = FindAnyObjectByType<SoundHandler>();
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

        hitsList.RemoveAll(x => !(x.collider.gameObject.CompareTag("WallPoint")||x.collider.gameObject.CompareTag("Anchor")));
        if (hitsList.Count<1) return;
        hitsList = hitsList.OrderBy(
            x => Vector3.Distance(new Vector3(ray.origin.x,x.transform.position.y,ray.origin.z),x.transform.position)
        ).ToList();

        Tile hitTile = null;
        if( !hitsList.First().collider.gameObject.TryGetComponent(out WallPointScript hitWallPoint )&&
            !hitsList.First().collider.gameObject.TryGetComponent(out  hitTile ))return;


        if (hitWallPoint is not null)
        {
            hitTile = hitWallPoint.linkedTile;
        }
        
        
        if (!firstPoint)
        {
            firstPoint = hitTile;
            return;
        }

        Ray checkRay = new Ray(firstPoint.transform.position,hitTile.transform.position-firstPoint.transform.position);

        if (Vector3.Distance(firstPoint.transform.position, hitTile.transform.position) >= maxWallSize *squareRootof2 ||
            Vector3.Distance(firstPoint.transform.position, hitTile.transform.position)<=1||
            Physics.RaycastAll(checkRay,Vector3.Distance(firstPoint.transform.position, hitTile.transform.position))
                .Any(x=>x.collider.gameObject.CompareTag("Wall")))
            
        {
            Debug.Log("illegal wall");
            //todo: implement
            firstPoint = null;
            return;
        }

        hitTile.isWall = true;
        firstPoint.isWall = true;

        CreateWall(firstPoint, hitTile); 
        firstPoint = null;
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
        wallScript.Create(one.currentWallPointScript,two.currentWallPointScript,gridManager,sculptor,soundHandler);
        
        firstPoint = null;
    }
    
    
    

    

    public WallPointScript CreateWallPoint(Tile tile)
    {
        
        GameObject newWallPoint= Instantiate(wallPointPrefab, tile.transform.position, Quaternion.identity);
        WallPointScript toReturn = newWallPoint.GetComponent<WallPointScript>();
        toReturn.Create(tile,soundHandler);
        return toReturn;
    }
    
    public WallPointScript ExtendWallPoint(Vector3 origine)
    {
        GameObject newWallPoint= Instantiate(wallPointPrefab, origine, Quaternion.identity);
        return newWallPoint.GetComponent<WallPointScript>();
    }

    public void ExtendWall(WallPointScript one ,WallPointScript two)
    {
        GameObject wall = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity);
        WallScript wallScript = wall.GetComponent<WallScript>();
        wallScript.Create(one,two,gridManager,sculptor,soundHandler);
    }
    
    
    
}
