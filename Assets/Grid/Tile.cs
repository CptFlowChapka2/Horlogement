using UnityEngine;

public class Tile : MonoBehaviour
{
   
   public Vector2Int coords;
   private GridManager gridManager;
   public bool isWall = false;
   
   public void Initialize(Vector2Int coords, GridManager gridManager)
   {
      this.coords = coords;
      this.gridManager = gridManager;
   }
   
  

   
   private void OnMouseDown()
   {
      Debug.Log("Clicked on " + gridManager.WorldToGridPos(transform.position));
   }
}
