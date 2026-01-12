using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sculptor : MonoBehaviour
{
    private WallCreator wallCreator;
    private Tile firstTile;
    private WallPointScript firstTilePoint;
    private WallPointScript secondTilePoint;
    public Vector3 cursorCheckSize = new Vector3(1, 30, 1);


    private void Start()
    {
        wallCreator = GetComponent<WallCreator>();
        cursorCheckSize = new Vector3(cursorCheckSize.x / 2, cursorCheckSize.y / 2, cursorCheckSize.z / 2);
    }
    private void Update()
    {
        DestoryWallOnClic();
        if (secondTilePoint is not null)
        {
            MoovePhantom();
        }
        Clic();
    }


    private void DestoryWallOnClic()
    {
        if (!Input.GetMouseButtonDown(1)) return;

        if (secondTilePoint is not null)
        {
            Destroy(secondTilePoint.gameObject);
            secondTilePoint = null;
            firstTile = null;
            Destroy(firstTilePoint.gameObject);
            firstTilePoint = null;
            return;
        }

        if (!SelectObjectByCursor(new []{"Wall"},out List<RaycastHit> hitsList)) return;
        

        if (hitsList.First().collider.gameObject.TryGetComponent(out WallScript hitWall))
        {
            WallPointScript one = hitWall.one;   
            WallPointScript two = hitWall.two;   
            hitWall.Break();
            one.CheckWalls();
            two.CheckWalls();
           
        }

    }

    private void Clic()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if(!SelectObjectByCursor(new []{"WallPoint","Anchor"},out List<RaycastHit> hitList))return;
        

        Tile hitTile = null;
        if( !hitList.First().collider.gameObject.TryGetComponent(out WallPointScript hitPoint )&&
            !hitList.First().collider.gameObject.TryGetComponent(out hitTile ))return;

        if (firstTile is null)
        {
            
            if (hitPoint is not null)
            {
                firstTilePoint = hitPoint;
                firstTile = firstTilePoint.linkedTile;
            }
            else if (hitTile is not null)
            {
                firstTile = hitTile;
                firstTilePoint=wallCreator.CreateWallPoint(firstTile);
            }

            secondTilePoint = wallCreator.ExtendWallPoint(firstTilePoint.transform.position+Vector3.up);
            wallCreator.ExtendWall(firstTilePoint,secondTilePoint);
            secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(Color.black));
            secondTilePoint.walls.ForEach(x=>x.ToggleColision(true));
            return;
        }
        if(secondTilePoint is null) return;

        if (secondTilePoint.walls.FindAll(x=>CheckForIntersection(x)).Count >= 1)
        {
            Destroy(secondTilePoint.gameObject);
            secondTilePoint = null;
            firstTile = null;
            Destroy(firstTilePoint.gameObject);
            firstTilePoint = null;
            return;
        }
        
        secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(Color.white));
        secondTilePoint.walls.ForEach(x=>x.ToggleColision(false));
        secondTilePoint.EndMouvement();
        firstTile = null;
        secondTilePoint = null;
        firstTilePoint = null;

    }

    private void MoovePhantom()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        secondTilePoint.transform.position = new Vector3(newPos.x, secondTilePoint.transform.position.y, newPos.z);
        
        secondTilePoint.walls.ForEach(x=>x.Moove(false));
        
        secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(Color.black));
        
        secondTilePoint.walls.FindAll(x=>CheckForIntersection(x)).ForEach(y=>y.SetFeedBackColor(Color.red));

        

    }

    private bool SelectObjectByCursor(string[] tagToSelect, out List<RaycastHit> toReturn)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.BoxCastAll(ray.origin,cursorCheckSize,ray.direction);
        
       List<RaycastHit> hitList = new List<RaycastHit>();
       toReturn = new List<RaycastHit>();

        if (hits.Length < 1) return false;
        Debug.Log("hit something");
        hitList = hits.ToList();
        
       

        List<string> tagToSelectList = tagToSelect.ToList();
        FilterCondition(hitList, tagToSelectList, out hitList);
        
        if (hitList.Count < 1) return false;
        Debug.Log("hit a valid thing");
        toReturn = hitList.OrderBy(x =>
            Vector3.Distance(new Vector3(ray.origin.x, x.transform.position.y, ray.origin.z), x.transform.position)
        ).ToList();

        return true;

    }

    private static void FilterCondition(List<RaycastHit> aList,List<string> yList,out List<RaycastHit> returnList)
    {
       
        returnList = aList.Where(a => yList.Any(y => a.collider.gameObject.CompareTag(y))).ToList();
    }

    private bool CheckForIntersection(WallScript wallToCheck)
    {
        
        Vector3 dimensionToCheck = new Vector3(wallToCheck.transform.localScale.x/2,wallToCheck.transform.localScale.x/2,(wallToCheck.length/2)*0.8f);
        List<Collider> boxCastList = Physics.OverlapBox(wallToCheck.transform.position,dimensionToCheck,wallToCheck.transform.rotation).ToList();
        boxCastList.RemoveAll(x => x==wallToCheck.thisCollider||!x.gameObject.CompareTag("Wall"));
        return boxCastList.Count > 0;

    }
}