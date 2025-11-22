using System;
using UnityEngine;

public class ContextReader : MonoBehaviour
{
    [Header("Context Info")]
     private bool isGrounded;
     private bool isOnGear;
     public GameObject currentGear; // Le gear touché

     public groundedType groundedType=groundedType.Null;
    public bool IsGrounded => isGrounded;
    public bool IsOnGear => isOnGear;
    public GameObject CurrentGear => currentGear;

    public JointManager jointManager;

   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = true; // S'il y a une collision avec le gameobject "Ground" qui est le sol alors isGrounded = True
            groundedType = groundedType.IsGrounded;
            currentGear = null;
        }
            
        else if (collision.gameObject.CompareTag("Gear"))
        {
            //isOnGear = true; // Si c'est une collision avec un Gear alors isOnGear = true et currentGear est le gameobject sur lequel on est.
            currentGear = collision.gameObject;
            jointManager.CreateJoint(currentGear);
            transform.up = currentGear.transform.up;  
            transform.forward = currentGear.transform.forward;
            groundedType = groundedType.IsOnGear;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            //isGrounded = false; // Lorsqu'on sort du gameobject Ground isGrounded devient false
            groundedType = groundedType.Airborn;
            currentGear = null;
        }
            
        else if (collision.gameObject.CompareTag("Gear"))
        {
            //isOnGear = false; // Et si on sort d'un gear alors isOnGear devient false et currentGear est reset.
            currentGear = null;
            jointManager.DestroyJoint();
            groundedType = groundedType.Airborn;
            
        }
    }
}

public enum groundedType
{
    Airborn,
    IsGrounded,
    IsOnGear,
    Null,
}