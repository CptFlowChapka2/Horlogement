using System;
using UnityEngine;

public class Pivot_AnchorCam : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform.position,Vector3.up);
    }
}
