using System;
using UnityEngine;

public class TEst1 : MonoBehaviour
{
    public GearPhysRotation GearPhysRotation;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GearPhysRotation.rotationSpeed += 10f;
        }
    }
}
