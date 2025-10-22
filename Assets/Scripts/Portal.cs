using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Portal : MonoBehaviour
{
    [Header("gameObject")] 
    [SerializeField] private GameObject portal;
    public GearPhysRotation GearPhysRotation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GearPhysRotation.countDown = Random.Range(5, 15);
            GearPhysRotation.canRotate = true;
            portal.SetActive(false);
        }
    }

 
}
