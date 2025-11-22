using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

 enum gearAxeLiberty
{
    X,
    Z,
    XZ
}

public class playerMove : MonoBehaviour
{
    [Header("Initialise")]
    [SerializeField] private Rigidbody rb;
    
   
        
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    
    
    

   

    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
    }
    
    //Gear
    public void GearPressedMove(Vector3 mouvement)
    {
        //todo 
    }

    public void GearSustainedMove(Vector3 mouvement)
    {
       //todo 
    }

    public void GearReleasedMove(Vector3 mouvement, GameObject gear)
    {
    }

    //AirBorne
    public void PresedJump()
    {
       
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        rb.angularVelocity = Vector3.zero;
    }

    public void SustainJump()
    {
    }

    public void ReleaseJump()
    {
    }

    public void AirSustainMove(Vector3 mouvement)
    {
        //todo 
    }

    public void AirReleaseMove()
    {
      
    }

    public void AirPressedMove(Vector3 mouvement)
    {
        //todo 
    }

    public void ApplyAirDecel()
    {
       //todo 
    }

    
    
    
}

