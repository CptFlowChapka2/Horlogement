using UnityEngine;



public class GearPhysRotation : MonoBehaviour
{
    
    [Header("Rotation")] 
    [SerializeField] public float rotationSpeed = 100f;
    private Vector3 rotationAxe=Vector3.up;
    
    private Rigidbody rb;




    private void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    private void FixedUpdate()

    {

        Quaternion rotation = Quaternion.Euler(this.rotationAxe.normalized*(Time.fixedDeltaTime*rotationSpeed));
        rb.MoveRotation(rb.rotation * rotation);

    }


}