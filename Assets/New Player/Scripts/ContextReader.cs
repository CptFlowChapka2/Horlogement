using System;
using UnityEngine;

public class ContextReader : MonoBehaviour
{
    [Header("Context Info")]
     public GameObject currentGear; // Le gear touché

     public groundedType groundedType=groundedType.Null;
    

   

   
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Gear"))
        {
            
            currentGear = collision.gameObject;
            groundedType = groundedType.IsOnGear;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gear"))
        {
          
            currentGear = null;
           
            groundedType = groundedType.Airborn;
            
        }
    }
}

public enum groundedType
{
    Airborn,
    IsOnGear,
    Null,
}