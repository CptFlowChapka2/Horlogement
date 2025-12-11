using TMPro;
using UnityEngine;

public class EntityScript : MonoBehaviour
{
    public float speed = 1f;

    private DataHolder dataHolder;
    private Rigidbody rb;
    private Vector3 currentDir;
    public bool secondColider;
    [SerializeField] private bool isDummy=false;
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

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            Vector3 surfaceNormal = other.GetContact(0).normal;
            Bounce(surfaceNormal);
        }

        if (other.gameObject.CompareTag("Entity") && !other.gameObject.GetComponent<EntityScript>().justCreated && !other.gameObject.GetComponent<EntityScript>().secondColider)
        {
            Debug.Log("collision");

            EntityScript otherEntityScript = other.gameObject.GetComponent<EntityScript>();
            secondColider = true;
            Vector3 otherLastVelocity = otherEntityScript.lastVelocity;
            identityKeys otherIdentity = otherEntityScript.thisIdentity.IdentityKey;
            
            
           
            Vector3 givenDir = lastVelocity;
           
            if (lastVelocity == Vector3.zero)
            {
                givenDir = thisIdentity.DefaultDirection;
            }

            //todo: noise
            GameObject fusedEntity=Instantiate(dataHolder.intantiateDummy, transform.position, Quaternion.identity);
            EntityScript fusedEntityScript=fusedEntity.GetComponent<EntityScript>();
            fusedEntityScript.justCreated=true;

            fusedEntityScript.isDummy = false;
            fusedEntityScript.speed = speed;
            Debug.Log(thisIdentity.IdentityKey);
            Debug.Log(otherIdentity);

            fusedEntityScript.OnCreation( thisIdentity.IdentityKey,otherIdentity, fusedEntityScript.CreateDir(givenDir, otherLastVelocity));
            Destroy(gameObject);
            Destroy(other.gameObject);

        }
    }

    private void OnCreation(identityKeys key, identityKeys key2 = default, Vector3 dir = default)
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

    private Vector3 CreateDir(Vector3 a, Vector3 b = default)
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