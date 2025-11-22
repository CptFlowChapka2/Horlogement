using System;
using UnityEngine;

public class TEst1 : MonoBehaviour
{
    public GearPhysRotation GearPhysRotation;
    public float xMax = 250f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GearPhysRotation.rotationSpeed += 10f;
        }

        if (GearPhysRotation.rotationSpeed >= 250f)
        {
            GearPhysRotation.rotationSpeed = 250f; 
        }
        
        

        
    }
}
