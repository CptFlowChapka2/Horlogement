using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;


public class GearPhysRotation : MonoBehaviour
{
    [Header("Rotation")] 
    [SerializeField] public float rotationSpeed = 100f;
    private Rigidbody rb;

    [Header("Time")] 
    [SerializeField] private float t;
    [SerializeField] public float countDown = 5f;

    [Header("Random Rotation")] 
    public bool canRotate = true;
    [SerializeField] private float randomNumber;

    [Header("Object")] 
    [SerializeField] private GameObject portal;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        countDown = UnityEngine.Random.Range(5f, 10f);
        portal.SetActive(false);
    }

    private void FixedUpdate()

    {
        if (canRotate)
        {
            Quaternion rotation = Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f);
            rb.MoveRotation(rb.rotation * rotation);
        }
    }

    private void Update()
    {
        if (canRotate)
        {
            countDown -= Time.deltaTime;
          
        }

        if (countDown <= 0)
        {
            //canRotate = false;
            portal.SetActive(true);
            rotationSpeed -= rotationSpeed * 0.5f+3;
        }
    }
}