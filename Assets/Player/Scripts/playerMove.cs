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
    [Header("Grounded Parameters")]
    [SerializeField] private float initialSpeed = 5f;
    [SerializeField] private float sustainedSpeed = 2f;
    [SerializeField] private float stopSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 10f; //MaxSpeed
    
    [Header("Airborne Parameters")]
    [SerializeField] private float maxAirMoveSpeed = 10f; //AirMaxSpeed
    [SerializeField] private float minAirMoveSpeed = 2f; //AIrMinSpeed
    [SerializeField] private float airSustainedSpeed = 0.5f;
    [SerializeField] private float airInitialSpeed = 0.5f;
    
    [SerializeField] private float airDecel = 1f;
    
    [SerializeField] private float maxAirAngle = 5f;
    
    [Header("Jump Parameters")]
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Gear Parameters")]
    [SerializeField] private gearAxeLiberty gearAxeLiberty=gearAxeLiberty.X;
    [SerializeField] private float gearInitialSpeed=1f;
    [SerializeField] private float gearSustainSpeed=0.5f;

    private Vector2 gearAxeControlMod=new Vector2(0,0);


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        switch (gearAxeLiberty)
        {
            case gearAxeLiberty.X:
                gearAxeControlMod = new Vector2(1, 0);
                break;
            case gearAxeLiberty.Z:
                gearAxeControlMod = new Vector2(0, 1);
                
                break;
            case gearAxeLiberty.XZ:
                gearAxeControlMod = new Vector2(1, 1);
                
                break;
        }
    }


    //Grounded
    public void SustainMove(Vector3 mouvement)
    {
        if (rb.linearVelocity.magnitude < maxMoveSpeed)
        {
            rb.AddRelativeForce(mouvement * sustainedSpeed, ForceMode.Force);
        }
    }

    public void ReleaseMove()
    {
        Vector3 stopVector = rb.linearVelocity.normalized * stopSpeed;
        rb.linearVelocity -= stopVector;
    }

    public void PressedMove(Vector3 mouvement)
    {
        rb.AddRelativeForce(mouvement * initialSpeed, ForceMode.Force);
    }

    
    //Gear
    public void GearPressedMove(Vector3 mouvement, JointManager jointManager)
    {
        if (jointManager.currentJoint is not null)
        {
            jointManager.MooveJoint(new Vector3(mouvement.x*gearAxeControlMod.x, 0, mouvement.z*gearAxeControlMod.y) * (Time.deltaTime * gearInitialSpeed));
        }
    }

    public void GearSustainedMove(Vector3 mouvement, JointManager jointManager)
    {
        if (jointManager.currentJoint is not null)
        {
            jointManager.MooveJoint(new Vector3(mouvement.x*gearAxeControlMod.x, 0, mouvement.z*gearAxeControlMod.y) * (Time.deltaTime*gearSustainSpeed));
        }
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
        if (rb.linearVelocity.magnitude < minAirMoveSpeed)
        {
            rb.linearVelocity=rb.linearVelocity.normalized*minAirMoveSpeed ;
        }
        
        if (Mathf.Abs(Vector3.Angle(rb.linearVelocity, transform.InverseTransformVector(mouvement))) >= maxAirAngle &&
            rb.linearVelocity.magnitude < maxAirMoveSpeed)
        {
            rb.AddRelativeForce(mouvement * airSustainedSpeed, ForceMode.Force);
        }
    }

    public void AirReleaseMove()
    {
        // Vector3 stopVector = rb.linearVelocity.normalized * stopSpeed;
        // rb.linearVelocity -= stopVector;
    }

    public void AirPressedMove(Vector3 mouvement)
    {
        if (Mathf.Abs(Vector3.Angle(rb.linearVelocity, transform.InverseTransformVector(mouvement))) >= maxAirAngle)
        {
            rb.AddRelativeForce(mouvement * airInitialSpeed, ForceMode.Force);
        }
    }

    public void ApplyAirDecel()
    {
        if (rb.linearVelocity.magnitude > minAirMoveSpeed)
        {
            
            Vector3 stopVector = rb.linearVelocity.normalized * (airDecel*Time.fixedDeltaTime);
            rb.linearVelocity -= stopVector;
        }
    }

    public void OrientPlayer()
    {
        transform.forward=rb.angularVelocity;
    }
}

