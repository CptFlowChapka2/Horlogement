using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float sustainedSpeed = 2f;
    [SerializeField] private float stopSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float maxAirAngle = 5f;
    [SerializeField] private float airSustainedSpeed = 0.5f;
    [SerializeField] private float airInitialSpeed = 0.5f;
    
    

    



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }





    public void SustainMove(Vector3 mouvement)
    {
        if (rb.linearVelocity.magnitude <maxMoveSpeed )
        {
            rb.AddRelativeForce(mouvement*sustainedSpeed,ForceMode.Force);
          
        }
        
        
    }
    public void ReleaseMove()
    {
        Vector3 stopVector = rb.linearVelocity.normalized*stopSpeed;
        rb.linearVelocity -= stopVector;

    }
    public void PressedMove(Vector3 mouvement)
    { 
        rb.AddRelativeForce(mouvement*initialSpeed,ForceMode.Force);
       
    }

    public void GearPressedMove(Vector3 mouvement,JointManager jointManager)
    {
        jointManager.MooveJoint(new Vector3(mouvement.x,0,0) * (Time.deltaTime * initialSpeed));
        
    }
    public void GearSustainedMove(Vector3 mouvement,JointManager jointManager)
    {
        jointManager.MooveJoint(new Vector3(mouvement.x,0,0) * (Time.deltaTime * sustainedSpeed));
        
    }
    public void GearReleasedMove(Vector3 mouvement,GameObject gear)
    {
        
    }

    public void PresedJump()
    {
        
        rb.AddForce(new Vector3(0,jumpForce,0),ForceMode.Impulse);
    }
    public void SustainJump()
    {
        
    }
    public void ReleaseJump()
    {
        
    }
    
    public void AirSustainMove(Vector3 mouvement)
    {
       

        if (Mathf.Abs(Vector3.Angle(rb.linearVelocity, transform.InverseTransformVector(mouvement))) >= maxAirAngle)
        {
            rb.AddRelativeForce(mouvement*airSustainedSpeed,ForceMode.Force);
        }
        
    }
    public void AirReleaseMove()
    {
        Vector3 stopVector = rb.linearVelocity.normalized*stopSpeed;
        rb.linearVelocity -= stopVector;

    }
    public void AirPressedMove(Vector3 mouvement)
    { 
        if (Mathf.Abs(Vector3.Angle(rb.linearVelocity, transform.InverseTransformVector(mouvement))) >= maxAirAngle)
        {
            rb.AddRelativeForce(mouvement*airInitialSpeed,ForceMode.Force);
        }
       
    }



}