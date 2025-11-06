using UnityEngine;

public class NewCam : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (player == null) return;


        Vector3 targetPosition = new Vector3(player.position.x + offset.x,
            player.position.y + offset.y, offset.z);


        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
    }
}