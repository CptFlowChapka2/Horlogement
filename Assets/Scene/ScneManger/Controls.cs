using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controls : MonoBehaviour
{
   public List<SpriteRenderer> textMesh;

   private SpriteRenderer spriteRenderer;
   public Sprite sprite1;
   public Sprite sprite2;
   public Sprite sprite3;


   private void Start()
   {
      spriteRenderer = GetComponent<SpriteRenderer>();
   }

   
   private void OnMouseEnter()
   {
      spriteRenderer.sprite = sprite2;
   }

   private void OnMouseExit()
   {
      spriteRenderer.sprite = sprite1;
   }
   private void OnMouseUpAsButton()
   {
      spriteRenderer.sprite = sprite3;
      textMesh.ForEach(x=>x.enabled=!x.enabled);
   }
}
