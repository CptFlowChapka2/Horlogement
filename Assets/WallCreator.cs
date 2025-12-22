using System;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public GameObject wallPrefab;
    public GameObject wallPointPrefab;

    private Tile firstPoint = null;
    public GridManager gridManager;
    private float squareRootof2 =(float)Math.Sqrt(2f) ;

    void Update()
    {
        DebugClicToWall();
    }
    private void DebugClicToWall()
    {

        if (!Input.GetMouseButtonDown(0)) return;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out RaycastHit hit) || !hit.collider.CompareTag("Anchor")) return;
        Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
        Debug.Log("Sphère cliquée : " + hit.collider.name);
        if (!firstPoint)
        {
            firstPoint = hitTile;
            return;
        }

        if (Vector3.Distance(firstPoint.transform.position, hit.collider.transform.position) >= (gridManager.tileSize.x*1.2f) *squareRootof2 )
        {
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

        Vector3 a = one.currentWallPointScript.transform.position; 
        Vector3 b = two.currentWallPointScript.transform.position; 
        Vector3 mid = (a + b) / 2f;
        GameObject wall = Instantiate(wallPrefab, mid, Quaternion.identity);

        Vector3 diff = b - a;
        diff.y = 0;

        float length = diff.magnitude;
        
        wall.transform.localScale = new Vector3(0.3f, 5f, length);
        wall.transform.rotation = Quaternion.LookRotation(diff);
        firstPoint = null;
    }

    

    public void CreateWallPoint(Tile tile)
    {
        GameObject newWallPoint= Instantiate(wallPointPrefab, tile.transform.position, Quaternion.identity);
        newWallPoint.GetComponent<WallPointScript>().Create(tile,gridManager);
    }
}
