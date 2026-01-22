
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


public class Sculptor : MonoBehaviour
{
    private WallCreator wallCreator;
    private DataHolder dataHolder;
    private GridManager gridManager;
    private Tile firstTile;
    private WallPointScript firstTilePoint;
    public WallPointScript secondTilePoint;
    public Vector3 cursorCheckSize = new Vector3(1, 30, 1);
    public SoundHandler soundHandler;
    private AudioClip wallCreate2;
    private AudioClip wallCreate1; 
    private Material illegalWall;
    private Material phantomlWall;
    private Material normallWall;
    private Camera camera1;
   

    private void Start()
    {
        
        camera1 = Camera.main;
        wallCreator = GetComponent<WallCreator>();
        cursorCheckSize = new Vector3(cursorCheckSize.x / 2, cursorCheckSize.y / 2, cursorCheckSize.z / 2);
        dataHolder = FindAnyObjectByType<DataHolder>();
        gridManager = FindAnyObjectByType<GridManager>();
        wallCreate1 = dataHolder.wallCreate1;
        wallCreate2 = dataHolder.wallCreate2;
        illegalWall = dataHolder.illegalWall;
        phantomlWall = dataHolder.phantomlWall;
    }
    
    private void Update()
    {
        DestoryWallOnClic();
        
        if(wallCreator.activateNewMode)return;
        gridManager.allTile.ForEach(x=>x.ChangeColor(x.off));
        if (secondTilePoint != null)
        {
            
            MoovePhantom();
        }
        Clic();
    }


    private void DestoryWallOnClic()
    {
        if (!SelectObjectByCursor(new []{"Wall"},out List<RaycastHit> hitsList)) return;

        if (!hitsList.First().collider.gameObject.TryGetComponent(out WallScript hitWall))return;
        
        
        if (!Input.GetMouseButtonDown(1)) return;

        if (secondTilePoint != null)//anulation de phatom
        {
            Destroy(secondTilePoint.gameObject);
            secondTilePoint = null;
            firstTile = null;
            Destroy(firstTilePoint.gameObject);
            firstTilePoint = null;
            return;
        }
        //destruction
        
            WallPointScript one = hitWall.one;   
            WallPointScript two = hitWall.two;   
            hitWall.Break();
            if(one is null ||two is null)return;
            one.CheckWalls();
            two.CheckWalls();
           
        

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
            secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(phantomlWall));
            secondTilePoint.walls.ForEach(x=>x.ToggleColision(true));
            
            // soundHandler.Play(firstTilePoint.gameObject,wallCreate1);
            return;
        }
        if(secondTilePoint is null) return;

        if (secondTilePoint.walls.FindAll(x=>CheckForIntersection(x)).Count >= 1)
        {
            Destroy(secondTilePoint.gameObject);
            secondTilePoint = null;
            firstTile = null;
            firstTilePoint.CheckWalls();
            firstTilePoint = null;
            return;
        }
        
        secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(normallWall));
        secondTilePoint.walls.ForEach(x=>x.ToggleColision(false));
        secondTilePoint.EndMouvement();
        soundHandler.Play(secondTilePoint.gameObject,wallCreate2);
        firstTile = null;
        secondTilePoint = null;
        firstTilePoint = null;

    }

    private void MoovePhantom()
    {
        Vector3 newPos = camera1.ScreenToWorldPoint(Input.mousePosition);
        secondTilePoint.transform.position = new Vector3(newPos.x, secondTilePoint.transform.position.y, newPos.z);
        
        secondTilePoint.walls.ForEach(x=>x.Moove(false));
        soundHandler.Moove(secondTilePoint.gameObject);
        
        secondTilePoint.walls.ForEach(x=>x.SetFeedBackColor(phantomlWall));
        
        secondTilePoint.walls.FindAll(x=>CheckForIntersection(x)).ForEach(y=>y.SetFeedBackColor(illegalWall));
        
        secondTilePoint.gridManager.allTile.ForEach(x=>x.ChangeColor(x.off));
        secondTilePoint.ProximityCheck(out Tile result);
        result.ChangeColor(result.on);



    }

    private bool SelectObjectByCursor(string[] tagToSelect, out List<RaycastHit> toReturn)
    {
        Ray ray = camera1.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.BoxCastAll(ray.origin,cursorCheckSize,ray.direction);
        
       List<RaycastHit> hitList = new List<RaycastHit>();
       toReturn = new List<RaycastHit>();

        if (hits.Length < 1) return false;
        
        hitList = hits.ToList();
        
       

        List<string> tagToSelectList = tagToSelect.ToList();
        FilterCondition(hitList, tagToSelectList, out hitList);
        
        if (hitList.Count < 1) return false;
        
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
        
        Vector3 dimensionToCheck = new Vector3(wallToCheck.transform.localScale.x/2,wallToCheck.transform.localScale.x/2,((wallToCheck.length-wallToCheck.transform.localScale.x)/2));
        Debug.DrawLine(Vector3.zero,wallToCheck.transform.position+
                                    (wallToCheck.transform.forward * 
                                     wallToCheck.transform.localScale.x));
        
        List<Collider> boxCastList = 
            Physics.OverlapBox(wallToCheck.transform.position+
                               (wallToCheck.transform.forward * 
                                wallToCheck.transform.localScale.x),dimensionToCheck,wallToCheck.transform.rotation).ToList();
        
        string[] keepTags = {"Wall", "Entity","EntityColor"};
        boxCastList.RemoveAll(x => x==wallToCheck.thisCollider||!keepTags.Contains(x.gameObject.tag));

        
        
        
        
        return boxCastList.Count > 0;

    }
}