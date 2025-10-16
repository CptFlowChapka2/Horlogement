using System;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    public GameObject target;



    private void Update()
    {
        transform.LookAt(target.transform.position,Vector3.up);
    }
}
