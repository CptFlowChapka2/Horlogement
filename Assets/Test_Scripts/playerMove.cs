using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Rigidbody rb;
    private inputReader inputReader;
    private float moveX; 
    
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        moveX = inputReader.horizontalMove;
        Debug.Log("Moving");
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveX * speed, rb.linearVelocity.y);
    }
}
