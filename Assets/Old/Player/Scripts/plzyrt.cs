using UnityEngine;

public class plzyrt : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float sliding = 3f;

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Gear"))
        {
            isGrounded = true;
            
        }

      
        if (collision.gameObject.CompareTag("Gear"))
        {
            transform.parent = collision.transform;
            rb.AddForce(Vector3.forward * sliding, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Gear"))
        {
            transform.parent = null;
            rb.AddForce(Vector3.forward * sliding, ForceMode.Impulse);
        }
    }
}