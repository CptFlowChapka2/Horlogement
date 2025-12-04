using System;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public float speed = 1f;

    private Rigidbody rb;
    public Vector3 initialDir=new Vector3(1,0,1);
    public bool firstColider = false;
    private Vector3 lastVelocity;

    private void Start()
    {
        
        Oncreation();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 surfaceNormal = other.GetContact(0).normal;
            Bounce(surfaceNormal);
        }

        if (other.gameObject.CompareTag("Entity")&&!other.gameObject.GetComponent<EntityScript>().firstColider)
        {
            //todo: noise
            firstColider = true;
            GameObject newEntity = Instantiate(gameObject,transform.position,Quaternion.identity);
            EntityScript newEntityScript = newEntity.GetComponent<EntityScript>();
            newEntityScript.initialDir = (this.initialDir + other.gameObject.GetComponent<EntityScript>().initialDir).normalized;
            Destroy(other.gameObject);
            newEntityScript.Oncreation();
            Destroy(gameObject);
        }    
    }

    public void Oncreation()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearDamping = 0;
        initialDir = initialDir.normalized;
        rb.AddForce(initialDir*speed,ForceMode.VelocityChange);
    }

    private void Update()
    {
        lastVelocity = rb.linearVelocity;
    }

    public void Bounce(Vector3 surfaceNormal)
    {
        rb.linearVelocity = Vector3.Reflect(lastVelocity, surfaceNormal);

    }
}
