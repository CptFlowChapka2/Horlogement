using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 inputKey;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private GameObject floor;

    private void Update()
    {
        inputKey = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (inputKey != Vector3.zero) // On check si le personnage est en mouvement.
        {
            transform.forward = inputKey; // Comme il est en mouvement on set son forward en fonction de sa rotation.
            
            
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

            AddSpeed(floor.GetComponent<GearPhysRotation>(),speed*1.2f);
            

        }

    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Finish"))
        {
            floor = other.gameObject;
        }
    }

    private void FixedUpdate()
    {


        // rb.linearVelocity = InputKey; // Méthode 1 / Mid tier peut s'améliorer. 
        // rb.AddForce(InputKey * 50); // Méthode 2 mais cette utilisation de force est useless. 
        rb.MovePosition(transform.position + inputKey * (speed * Time.fixedDeltaTime));


    }


    public void AddSpeed(GearPhysRotation target,float speed)
    {
        target.rotationSpeed += speed;
    }
}
// D'ailleurs tu vas voir souvent ça dans mes scritps mais je chill hein je teste plein de méthodes différentes ça me permet d'apprendre donc fais pas gaffe je les commente généralement. 