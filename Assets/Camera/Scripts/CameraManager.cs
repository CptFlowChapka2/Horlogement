using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject camera;
    private GameObject cinemachineCameraObject;
    private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer cinemachinePositionComposer;
    
    [Header("Player Rotation")]
    [SerializeField] private float thirdPersonLookSensitivity = 1.5f;

    public GameObject pivot;

    private void Start()
    {
        
        camera=GameObject.FindGameObjectWithTag("MainCamera");
        cinemachineCamera = GameObject.FindAnyObjectByType<CinemachineCamera>();
        cinemachineCameraObject = cinemachineCamera.gameObject;
        cinemachinePositionComposer = cinemachineCamera.GetComponent<CinemachinePositionComposer>();
    }

    public void ChangeCameraTarget(GameObject newTarget)
    {
        if (newTarget.transform == cinemachineCamera.Follow)
        {
            return;
        }

        cinemachineCamera.Follow = newTarget.transform;
        
    }

    public void ChangeLookAhead(float time,float smoothing,bool canLookahead,bool ignoreY)
    {
        cinemachinePositionComposer.Lookahead.Enabled = canLookahead;
        cinemachinePositionComposer.Lookahead.IgnoreY = ignoreY;
        cinemachinePositionComposer.Lookahead.Time = time;
        cinemachinePositionComposer.Lookahead.Smoothing = smoothing;

    }

    public void ThirdPersonCameraOrient(Vector3 mouse)
    {
        pivot.transform.Rotate(new Vector3(mouse.y,mouse.x,0)*thirdPersonLookSensitivity,Space.Self);
    }
}
