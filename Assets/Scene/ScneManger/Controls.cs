using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Controls : MonoBehaviour
{
   public List<TextMeshProUGUI> textMesh;



   private void OnMouseUpAsButton()
   {
      textMesh.ForEach(x=>x.enabled=!x.enabled);
   }
}
