using UnityEngine;



public class GearPhysRotation : MonoBehaviour
{
    [Header("Rotation")] [SerializeField] public float rotationSpeed = 100f;
    private Rigidbody rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    private void FixedUpdate()

    {

        Quaternion rotation = Quaternion.Euler(0f, rotationSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * rotation);

    }


}