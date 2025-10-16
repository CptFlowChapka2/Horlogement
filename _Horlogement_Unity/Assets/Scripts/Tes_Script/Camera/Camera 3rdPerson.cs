using System;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    public GameObject playerPivot;



    private void Update()
    {
        transform.LookAt(playerPivot.transform.position,Vector3.up);
    }
}
