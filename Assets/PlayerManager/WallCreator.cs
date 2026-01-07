using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject wallPointPrefab;

    private Tile firstPoint = null;
    public GridManager gridManager;
    private float squareRootof2 =(float)Math.Sqrt(2f) ;
    public float maxWallSize = 1;

    void Update()
    {
        DebugClicToWall();
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
        Debug.Log(hitTile.coords);
        
        if (!firstPoint)
        {
            firstPoint = hitTile;
            return;
        }

        if (Vector3.Distance(firstPoint.transform.position, hitTile.transform.position) >= maxWallSize *squareRootof2 )
        {
            Debug.Log("hi");
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
        wallScript.Create(one.currentWallPointScript,two.currentWallPointScript,gridManager);
        
        firstPoint = null;
    }
    
    
    

    

    public void CreateWallPoint(Tile tile)
    {
        GameObject newWallPoint= Instantiate(wallPointPrefab, tile.transform.position, Quaternion.identity);
        newWallPoint.GetComponent<WallPointScript>().Create(tile);
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
        wallScript.Create(one,two,gridManager);
    }
    
    
    
}
