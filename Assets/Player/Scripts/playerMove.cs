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


}