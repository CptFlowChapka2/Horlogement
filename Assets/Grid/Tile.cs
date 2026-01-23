
using UnityEngine;

public class Tile : MonoBehaviour
{
   
   public Vector2Int coords;
   private GridManager gridManager;
   public bool isWall = false;
   public WallPointScript currentWallPointScript;
   private MeshRenderer meshRenderer;
   public Color off=Color.white;
   public Color on=Color.yellow;
   
   
   
   
   public void Initialize(Vector2Int coords, GridManager gridManager,DataHolder newDataHolder)
   {
      meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>(); 
      transform.Rotate(Vector3.up,Random.Range(0f,359f));
      this.coords = coords;
      this.gridManager = gridManager;
   }

   public void ChangeColor(Color color)
   {
      meshRenderer.material.color = color;
   }

   
}
