
using System.Collections.Generic;
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

   private MeshFilter meshFilter;
   public List<Mesh> randomMeshs = new List<Mesh>();
   
   
   
   
   public void Initialize(Vector2Int coords, GridManager gridManager,DataHolder newDataHolder)
   {
      
      meshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
      meshFilter = meshRenderer.gameObject.GetComponent<MeshFilter>();
      meshFilter.mesh = randomMeshs[Random.Range(0, randomMeshs.Count)];
      this.coords = coords;
      this.gridManager = gridManager;
   }

   public void ChangeColor(Color color)
   {
      meshRenderer.material.color = color;
   }

   
}
