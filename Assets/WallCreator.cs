using System;
using UnityEngine;

public class WallCreator : MonoBehaviour
{
    public GameObject wallPrefab;

    private Tile firstPoint = null;
    public GridManager gridManager;
    private float squareRootof2 =(float)Math.Sqrt(2f) ;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit)&&hit.collider.CompareTag("Anchor"))
            {
                Tile hitTile = hit.collider.gameObject.GetComponent<Tile>();
                Debug.Log("Sphère cliquée : " + hit.collider.name);
                if (firstPoint == null)
                {
                        firstPoint = hitTile;
                        return;
                }

                if (Vector3.Distance(firstPoint.transform.position, hit.collider.transform.position) >= (gridManager.tileSize.x*2 )*squareRootof2 )
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
        }
    }

     public void CreateWall(Tile one, Tile two)
    {
        if (one is null || two is null)
        {
            return;
        }

        Vector3 a = one.transform.position; 
        Vector3 b = one.transform.position; 
        Vector3 mid = (a + b) / 2f;
        GameObject wall = Instantiate(wallPrefab, mid, Quaternion.identity);

        Vector3 diff = b - a;
        diff.y = 0;

        float length = diff.magnitude;
        
        wall.transform.localScale = new Vector3(1f, 5f, length);
        wall.transform.rotation = Quaternion.LookRotation(diff);
    }
}
