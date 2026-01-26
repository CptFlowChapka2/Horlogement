
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
   
   public Vector2Int coords;
   private GridManager gridManager;
   public bool isWall = false;
   public WallPointScript currentWallPointScript;
   private SpriteRenderer spriteRenderer;
   public Color off=Color.white;
   public Color on=Color.yellow;

   private MeshFilter meshFilter;
   public List<Sprite> randomSprites = new List<Sprite>();
   
   
   
   
   public void Initialize(Vector2Int coords, GridManager gridManager,DataHolder newDataHolder)
   {
      
      
      this.coords = coords;
      this.gridManager = gridManager;
      spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
      spriteRenderer.sprite = randomSprites[Random.Range(0, randomSprites.Count)];
   }

   

   
}
