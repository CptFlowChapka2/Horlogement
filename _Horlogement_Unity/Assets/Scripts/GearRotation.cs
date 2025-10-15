using System;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    public float rotationSpeed = 5f;

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed) * Time.deltaTime); // Just doing a rotation in the z axis.
    }
}
