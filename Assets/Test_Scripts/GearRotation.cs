using System;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    [Header("Rotation")] 
    [SerializeField] private Quaternion rotation = new Quaternion(0.2f, 0.3f, -0.2f, 0.5f);
    [SerializeField] private float rotationSpeed = 100f;


    private void Update()
    {
        Quaternion rotation = Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
        transform.rotation *= rotation;
    }
}
