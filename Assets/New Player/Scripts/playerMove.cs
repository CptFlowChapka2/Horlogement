using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMove : MonoBehaviour
{
    [Header("Initialise")]
    [SerializeField] private Rigidbody rb;
    
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Gear Parameters")]
    [SerializeField] private float sustainedSpeed = 1f;
    [SerializeField] private float pressedSpeed = 0.5f;
    [SerializeField] private float realeasedSpeed = 2f;
    [SerializeField] private float decelSpeed = 2f;
    [SerializeField] private float maxPlayerSpeed = 2f;

    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        


    }

   
    
    //Gear
    public void GearPressedMove(Vector3 mouvement)
    {
        rb.AddForce(mouvement*pressedSpeed,ForceMode.Impulse);
    }

    public void GearSustainedMove(Vector3 mouvement)
    {
        rb.AddForce(mouvement*sustainedSpeed,ForceMode.Force);
    }

    public void GearReleasedMove(Vector3 mouvement)
    {
        
    }
    public void GearNothingMove()
    {
        Vector3 decel = Vector3.ClampMagnitude(new Vector3(-rb.linearVelocity.x, 0, -rb.linearVelocity.z), decelSpeed);
        
        rb.AddForce(decel); 
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

