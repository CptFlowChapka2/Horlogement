using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
   
   public Vector2Int coords;
   private GridManager gridManager;
   public bool isWall = false;
   public WallPointScript currentWallPointScript;
   
   
   
   
   public void Initialize(Vector2Int coords, GridManager gridManager,DataHolder newDataHolder)
   {
      this.coords = coords;
      this.gridManager = gridManager;
   }

   
}
