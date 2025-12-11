using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public float speed = 1f;

    private DataHolder dataHolder;
    private CollisionHandler collisionHandler;
    private Rigidbody rb;
    private Vector3 currentDir;
    public bool secondColider;
     public bool isDummy=false;
    private Vector3 lastVelocity;

    public bool initialCreated;
    public identityKeys initialDefault;
    private EntityIdentity thisIdentity = new EntityIdentity();
    public bool justCreated;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialCreated = false;
        dataHolder = FindAnyObjectByType<DataHolder>();
        collisionHandler = FindAnyObjectByType<CollisionHandler>();

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 surfaceNormal = other.GetContact(0).normal;
            Bounce(surfaceNormal);
        }

        if (other.gameObject.CompareTag("Entity") && !other.gameObject.GetComponent<EntityScript>().justCreated)
        {
            EntityScript otherEntityScript = other.gameObject.GetComponent<EntityScript>();
            List<object> parameterList = new List<object>();

            
            parameterList.Add(dataHolder);
            List<Vector3> vector3s = new List<Vector3>();
            vector3s.Add((transform.position+other.gameObject.transform.position)/2);
            
            Vector3 givenDir = lastVelocity;

            if (givenDir == Vector3.zero)
            {
                givenDir = thisIdentity.DefaultDirection;
            }
            vector3s.Add(givenDir);
            
            Vector3 otherGivenDir = otherEntityScript.lastVelocity;

            if (otherGivenDir == Vector3.zero)
            {
                otherGivenDir = otherEntityScript.thisIdentity.DefaultDirection;
            }
            vector3s.Add(otherGivenDir);
            
            parameterList.Add(vector3s);

            float givenSpeed = speed;
            parameterList.Add(givenSpeed);

            List<identityKeys> identityKeysList = new List<identityKeys>();
            identityKeysList.Add(thisIdentity.IdentityKey);
            identityKeysList.Add(otherEntityScript.thisIdentity.IdentityKey);
            
            parameterList.Add(identityKeysList);

            collisionHandler.AddToProcess(parameterList);
            Destroy(gameObject);
            Destroy(other.gameObject);



        }
    }

    public void OnCreation(identityKeys key, identityKeys key2 = default, Vector3 dir = default)
    {
        
        
        initialCreated = false;
        dataHolder = FindAnyObjectByType<DataHolder>();
        thisIdentity = new EntityIdentity();
        thisIdentity.Create(dataHolder,key, key2);
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        secondColider = false;

        
        rb.maxLinearVelocity = speed;

        Invoke(nameof(FalseJustCreated), Time.fixedDeltaTime);
        rb.linearDamping = 0;

        

        initialDefault = thisIdentity.IdentityKey;

        if (!isDummy)
        {
            if (dir == default || dir == Vector3.zero)
            {

                rb.AddForce(thisIdentity.DefaultDirection * speed, ForceMode.VelocityChange);
                return;
            }

            rb.AddForce(dir * speed, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {

        if (initialCreated)
        {

            OnCreation(initialDefault);
        }

        lastVelocity = rb.linearVelocity;
    }

    private void Bounce(Vector3 surfaceNormal)
    {
        rb.linearVelocity = Vector3.Reflect(lastVelocity, surfaceNormal);

    }

    public Vector3 CreateDir(Vector3 a, Vector3 b = default)
    {
        Vector3 newDir;
        if (b == default)
        {
            newDir = Vector3.zero;

            return newDir;
        }

        if (b == Vector3.zero)
        {
            return a;
        }

        if (a == Vector3.zero)
        {
            return b;
        }

        newDir = (a + b).normalized;
        return newDir;
    }
    private void FalseJustCreated()
    {
        justCreated = false;
    }
    

}