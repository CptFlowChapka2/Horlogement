using System;
using UnityEngine;

public class PivotCam : MonoBehaviour
{
    public GameObject player;
    public GameObject pivot;
    private void LateUpdate()
    {
        transform.position = pivot.transform.position;
        transform.LookAt(player.transform);
        
    }
}
