using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class playerMove : MonoBehaviour
{
    [Header("Initialise")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private JointManager jointManager;
    
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Gear Parameters")]
    [SerializeField] private float sustainedSpeed = 1f;
    [SerializeField] private float pressedSpeed = 0.5f;
    [SerializeField] private float realeasedSpeed = 2f;
    [SerializeField] private float decelSpeed = 0.1f;
    [SerializeField] private float maxPlayerSpeed = 15f;
     private float dimReturnPlayerSpeed = 0.9f;//magic number due to curve

     [Header("Airborn Parameters")]
     [SerializeField] private float airSustainedSpeed = 1f;
     [SerializeField] private float airPressedSpeed = 0.5f;
     [SerializeField] private float airRealeasedSpeed = 2f;
     [SerializeField] private float airDecelWhenTurning = 2f;
     [SerializeField] private float airMaxTurnRate = 2f;
     
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jointManager = GetComponent<JointManager>();




    }


    
    
    //Gear
    public void GearPressedMove(Vector3 mouvement)
    {
        if (jointManager.currentJoint is not null)
        {
            jointManager.MooveJointOrder(mouvement*pressedSpeed);
        }
        
    }

    public void GearSustainedMove(Vector3 mouvement)
    {
        if (jointManager.currentJoint is not null)
        {
            jointManager.MooveJointOrder(mouvement*sustainedSpeed);
        }
    }

    public void GearReleasedMove(Vector3 mouvement)
    { 
        if (jointManager.currentJoint is not null)
        {
            jointManager.DecelMooveJointOrder(decelSpeed);
        }
    }
    public void GearNothingMove()
    {
        

    }

    //AirBorne
    public void PresedJump()
    {
       jointManager.DestroyJoint();
       rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.VelocityChange);
        
    }

    public void SustainJump()
    {
    }

    public void ReleaseJump()
    {
    }

    public void AirSustainMove(Vector3 mouvement)
    {
        Vector3 cacheLinearVelocityY = new Vector3(0, rb.linearVelocity.y, 0);
        Vector3 rotatedXZ = Vector3.RotateTowards(new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z),mouvement*airSustainedSpeed,airMaxTurnRate,airDecelWhenTurning);
        rb.linearVelocity = rotatedXZ+cacheLinearVelocityY;
    }

    public void AirReleaseMove()
    {
      
    }

    public void AirPressedMove(Vector3 mouvement)
    {
        Vector3 cacheLinearVelocityY = new Vector3(0, rb.linearVelocity.y, 0);
        Vector3 rotatedXZ = Vector3.RotateTowards(new Vector3(rb.linearVelocity.x,0,rb.linearVelocity.z),mouvement*airPressedSpeed,airMaxTurnRate,airDecelWhenTurning);
        rb.linearVelocity = rotatedXZ+cacheLinearVelocityY;
        
    }

    public void ApplyAirDecel()
    {
       //todo 
    }

    
    
    
}

