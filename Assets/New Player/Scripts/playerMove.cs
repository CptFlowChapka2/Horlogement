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

        


    }


    private bool stableSpeed(Vector3 mouv,out Vector3 modMouv)
    {
        float velocityWithoutGear = rb.linearVelocity.magnitude - (rb.linearVelocity.magnitude -  pressedSpeed);
        Debug.Log(pressedSpeed-velocityWithoutGear);
        if (velocityWithoutGear <=pressedSpeed)
        {
            modMouv =mouv.normalized* Mathf.Clamp( pressedSpeed-velocityWithoutGear,-sustainedSpeed,sustainedSpeed);
            
            return true;
        }
        
        modMouv = mouv;
        return false;
        
    }
    
    //Gear
    public void GearPressedMove(Vector3 mouvement)
    {
        
        rb.AddForce(mouvement*pressedSpeed,ForceMode.VelocityChange);
    }

    public void GearSustainedMove(Vector3 mouvement)
    {
        Vector3 mouv;
        if (stableSpeed(mouvement, out mouv))
        {
            
           rb.AddForce(mouv,ForceMode.VelocityChange); 
        }
        
    }

    public void GearReleasedMove(Vector3 mouvement)
    {
        
    }
    public void GearNothingMove()
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
        rb.linearVelocity = Vector3.RotateTowards(rb.linearVelocity,mouvement*airSustainedSpeed,airMaxTurnRate,airDecelWhenTurning);
    }

    public void AirReleaseMove()
    {
      
    }

    public void AirPressedMove(Vector3 mouvement)
    {
        rb.linearVelocity = Vector3.RotateTowards(rb.linearVelocity,mouvement*airPressedSpeed,airMaxTurnRate,airDecelWhenTurning);
        
    }

    public void ApplyAirDecel()
    {
       //todo 
    }

    
    
    
}

