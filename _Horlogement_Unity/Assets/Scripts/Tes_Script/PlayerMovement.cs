using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Rigidbody rb;
    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }
}