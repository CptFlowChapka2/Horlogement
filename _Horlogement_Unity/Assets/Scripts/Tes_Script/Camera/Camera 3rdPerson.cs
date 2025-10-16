using System;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
    public GameObject target;
    public Vector3 distToPivot=new Vector3(0,0,-7f);

    private void Start()
    {
        transform.localPosition = distToPivot;
        
    }

    private void Update()
    {
        transform.LookAt(target.transform.position,Vector3.up);
    }
}
