using UnityEngine;



public class GearPhysRotation : MonoBehaviour
{
    
    [Header("Rotation")] 
    [SerializeField] public float rotationSpeed = 100f;
    [SerializeField] public Vector3 rotationAxe=Vector3.zero;//preferably only 1 ,-1 or 0
    
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