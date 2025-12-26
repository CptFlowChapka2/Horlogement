using System;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
   private bool playVoid;

   private void Start()
   {
      playVoid = !gameObject.CompareTag("Void");
   }

   private void OnTriggerStay(Collider other)
   {
      if (!other.gameObject.CompareTag("Entity") || !other.gameObject.TryGetComponent(out EntityScript otherScript)) return;
      if (playVoid)
      {
         otherScript.inPLay = true;
      }
      else
      {
         otherScript.inVoid = true;
      }
   }
   private void OnTriggerExit(Collider other)
   {
      if (!other.gameObject.CompareTag("Entity") || !other.gameObject.TryGetComponent(out EntityScript otherScript)) return;
      if (playVoid)
      {
         otherScript.inPLay = false;
      }
      else
      {
         otherScript.inVoid = false;
      }
   }
}

