using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public float speed = 1f;
    public float voidSpeed = 2f;
    public float invulTime = 1.5f;

    private DataHolder dataHolder;
    private CollisionHandler collisionHandler;
    private Rigidbody rb;
    private Vector3 currentDir;
    private GameObject child;
    
     
    private Vector3 lastVelocity;

    public bool initialCreated;
    public identityKeys initialDefault;
    public EntityIdentity thisIdentity = new EntityIdentity();
    public bool justCreated;
    public AudioSource audioSource;

    public bool inPLay = false;
   

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialCreated = false;
        dataHolder = FindAnyObjectByType<DataHolder>();
        collisionHandler = FindAnyObjectByType<CollisionHandler>();
        audioSource=GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            if (inPLay)
            {
                audioSource.PlayOneShot(thisIdentity.Sound);
            }
            Vector3 surfaceNormal = other.GetContact(0).normal;
            Bounce(surfaceNormal);
        }

        if (!justCreated&&other.gameObject.CompareTag("Entity") && other.gameObject.TryGetComponent<EntityScript>(out EntityScript otherEntityScript)&&!otherEntityScript.justCreated)
        {

            if (thisIdentity.IdentityKey == default || otherEntityScript.thisIdentity.IdentityKey == default)
            {
                return;
            }
            Vector3 givenDir = lastVelocity;

            if (givenDir == Vector3.zero)
            {
                givenDir = thisIdentity.DefaultDirection;
            }
            Vector3 otherGivenDir = otherEntityScript.lastVelocity;
            if (otherGivenDir == Vector3.zero)
            {
                otherGivenDir = otherEntityScript.thisIdentity.DefaultDirection;
            }
            HashSet<object> parameterList = new HashSet<object>
            {
                dataHolder,
                (transform.position + other.gameObject.transform.position) / 2,
                givenDir,
                otherGivenDir,
                speed,
                thisIdentity.IdentityKey,
                otherEntityScript.thisIdentity.IdentityKey
            };


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
        

        
        rb.maxLinearVelocity = speed;

        
        rb.linearDamping = 0;

        

        initialDefault = thisIdentity.IdentityKey;
        Debug.Log(thisIdentity.Color);
        child =transform.GetChild(0).Find("Color").gameObject ;
        
        child.GetComponent<MeshRenderer>().material=thisIdentity.Color;
        child.GetComponent<TrailRenderer>().material=thisIdentity.Color;

        if (gameObject.tag.Equals("Entity"))
        {
            if (dir == default || dir == Vector3.zero)
            {

                rb.AddForce(thisIdentity.DefaultDirection * speed, ForceMode.VelocityChange);
                return;
            }

            rb.AddForce(dir * speed, ForceMode.VelocityChange);
            
        }
        Invoke(nameof(FalseJustCreated), invulTime*Time.fixedDeltaTime);

        
    }

    private void FixedUpdate()
    {
        child.TryGetComponent(out SphereCollider sphereCollider);
        if (justCreated  )
        {
            sphereCollider.isTrigger = true;
        }
        else
        {
            sphereCollider.isTrigger = false;
        }

        
        
        if (initialCreated)
        {

            OnCreation(initialDefault);
        }

        if (inPLay&& !Mathf.Approximately(rb.linearVelocity.magnitude, speed))
        {
            rb.linearVelocity =rb.linearVelocity.normalized * speed;
        }
        else if (Mathf.Approximately(rb.linearVelocity.magnitude, voidSpeed))
        {
            rb.linearVelocity =rb.linearVelocity.normalized * voidSpeed;
        }

        transform.forward = -(transform.position- (transform.position+rb.linearVelocity.normalized));

        lastVelocity = rb.linearVelocity;
    }

    private void Bounce(Vector3 surfaceNormal)
    {
        Debug.Log(gameObject.name+" just bounced ");
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

    
    private void OnMouseUp()
    {
        initialCreated = true;
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Killed By outside the cam");
        Destroy(gameObject);
    }
    
    private GameObject FindChildWithTag(GameObject parent, string tag) {
        GameObject child = null;

        foreach(Transform transform in parent.transform) {
            if(transform.CompareTag(tag)) {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }


    // private void OnMouseDrag()
    // {
    //     transform.position += new Vector3(Input.mousePositionDelta.x, 0, Input.mousePositionDelta.y) * 0.1f;
    // }

}