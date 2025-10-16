using UnityEngine;

public class CameraAnchorCam : MonoBehaviour
{
    public GameObject target;

    private void Update()
    {
        transform.LookAt(target.transform.position,Vector3.up);
    }
}
