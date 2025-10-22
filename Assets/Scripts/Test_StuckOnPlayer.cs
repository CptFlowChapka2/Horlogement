using System;
using Unity.Cinemachine;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class Test_StuckOnPlayer : MonoBehaviour
{
    public GameObject target;
    public GameObject anchor;
    private SphereCollider thisSC;
    public GameObject cam;
    private CinemachineSplineDolly camSpline;
    public float maxOffset = 10f;




    private void Start()
    {
        thisSC = GetComponent<SphereCollider>();
        camSpline = cam.GetComponent<CinemachineSplineDolly>();
    }
    private void Update()
    {
        transform.position = target.transform.position;

        anchor.transform.position = thisSC.ClosestPointOnBounds(camSpline.Spline.EvaluatePosition(camSpline.CameraPosition));
        //camSpline.ForceCameraPosition(anchor.transform.position,cam.transform.rotation);
        


    }
   
}