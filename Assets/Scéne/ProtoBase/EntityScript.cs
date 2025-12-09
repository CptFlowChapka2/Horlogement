using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public float speed = 1f;

    private DataHolder dataHolder;
    private Rigidbody rb;
    private Vector3 currentDir;
    public bool firstColider = false;
    private Vector3 lastVelocity;
   
    public bool initialCreated=false;
    public Color initialDefault;
    private EntityIdentity thisIdentity=new EntityIdentity();
    public bool justCreated = false;

    

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialCreated = false;
        dataHolder=FindAnyObjectByType<DataHolder>();
        
    }
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 surfaceNormal = other.GetContact(0).normal;
            Bounce(surfaceNormal);
        }

        if (other.gameObject.CompareTag("Entity")&&!other.gameObject.GetComponent<EntityScript>().firstColider&&
            !other.gameObject.GetComponent<EntityScript>().justCreated)
        {
            //todo: noise
            firstColider = true;
            GameObject newEntity = Instantiate(gameObject,transform.position,Quaternion.identity);
            EntityScript newEntityScript = newEntity.GetComponent<EntityScript>();
            EntityScript otherEntityScript = other.gameObject.GetComponent<EntityScript>();
            newEntityScript.justCreated = true;
            newEntityScript.OnCreation(thisIdentity.Color,otherEntityScript.thisIdentity.Color,
                CreateDir(lastVelocity,otherEntityScript.lastVelocity));
            Destroy(other.gameObject);
            Destroy(gameObject);
        }    
    }

    private void OnCreation(Color key,Color key2=default,Vector3 dir=default)
    {
        dataHolder=FindAnyObjectByType<DataHolder>();
        rb = GetComponent<Rigidbody>();
        initialCreated = false;
        thisIdentity = new EntityIdentity();
        rb.maxLinearVelocity = speed;
        
        Invoke(nameof(FalseJustCreated),Time.fixedDeltaTime);
        rb.linearDamping = 0;

        Color keyComplete =dataHolder.allColor.Find(x=>x==key) ;
        Color key2Complete =dataHolder.allColor.Find(x=>x==key2) ;
        thisIdentity.Create(dataHolder,keyComplete,key2Complete);
        initialDefault = thisIdentity.Color;

        if (dir==default)
        {
            
            rb.AddForce(thisIdentity.DefaultDirection*speed,ForceMode.VelocityChange);
            return;
        }

        
        rb.AddForce(dir*speed,ForceMode.VelocityChange);
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

    private Vector3 CreateDir(Vector3 a,Vector3 b= default)
    {
        Vector3 newDir ;
        if (b==default)
        {
            return a;
        }

        newDir = (a + b).normalized;
        return newDir;
    }
    private void FalseJustCreated()
    {
        justCreated = false;
    }

    
}
