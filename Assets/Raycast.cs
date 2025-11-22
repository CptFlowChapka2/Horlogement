using System;
using Unity.VisualScripting;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject raycast;
    public LayerMask layerMask;
    public Color color = Color.yellow;
    public float distance = 10f;

    private void Update()
    {
        // if(Physics.Raycast(Vector3.forward)) 
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward),layerMask));
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, color);
            
        }
    }
}
