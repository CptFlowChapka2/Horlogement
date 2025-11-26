using System;
using UnityEngine;

public class JointManager : MonoBehaviour
{

    public FixedJoint currentJoint;
    public float breakForce;
    public Vector3 targetMoove;
    
    private bool travelling;
    private bool decelerating;
    public float speedMod=1f;
    private float decelspeed=0f;
    private int mooveIteration = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void FixedUpdate()
    {
        
        if (currentJoint is not null&&travelling)
        {
            MooveJoint();
            if (decelerating)
            {
                DecelMooveJoint();
            }
        }
        
    }

    public void CreateJoint(GameObject gear)
    {
        
        currentJoint = gameObject.AddComponent<FixedJoint>();
        currentJoint.connectedBody = gear.GetComponent<Rigidbody>();
        currentJoint.enableCollision = true;
        currentJoint.autoConfigureConnectedAnchor = true;
        currentJoint.enablePreprocessing = false;

        currentJoint.breakForce = breakForce;
        
        

    }

    public void MooveJointOrder(Vector3 moove)
    {
        mooveIteration++;
        if (mooveIteration > 30)
        {
             FixedJoint cacheJoint = currentJoint;
                    CreateJoint(currentJoint.connectedBody.gameObject);
                    DestroyJoint(cacheJoint);
                    mooveIteration = 0;
        }
        
       
        moove = -moove;
        if (currentJoint is null)
        {
            return;
        }
        if (currentJoint.autoConfigureConnectedAnchor )
        {
            Vector3 cacheAnchor = currentJoint.connectedAnchor;
                    currentJoint.connectedAnchor = cacheAnchor;
                    currentJoint.autoConfigureConnectedAnchor = false;
                    
        }




        
        targetMoove =currentJoint.anchor+moove;
        
        travelling = true;
        speedMod = 1f;
        MooveJoint();
    }
    private void MooveJoint()
    {
        if (currentJoint is null)
        {
            return;
        }
        if (currentJoint is not null&&Vector3.Distance(currentJoint.anchor,targetMoove)<=0.01)
        {
            speedMod = 1;
            decelspeed = 0;
            travelling = false;
            decelerating = false;
            targetMoove = currentJoint.anchor;
            return;
        }

        if (currentJoint is not null)
        {
            currentJoint.anchor = 
            Vector3.MoveTowards(currentJoint.anchor, targetMoove,  speedMod * Time.fixedDeltaTime);
            
        }


    }
    public void DecelMooveJointOrder(float speed)
    {
        decelspeed = speed;
        decelerating = true;
        DecelMooveJoint();
    }
    private void DecelMooveJoint()
    {
        speedMod = Mathf.Clamp(speedMod-decelspeed,0,1); 
    }

    public void DestroyJoint(FixedJoint joint)
    {
        
        
        if (currentJoint is not null)
        {
            Destroy(joint);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.CompareTag("Gear"))
        {
            
            
            CreateJoint(other.gameObject);
        }
    }
    private void OnCollisionExit(Collision other)
    {
        
        if (other.gameObject.CompareTag("Gear"))
        {
            
            travelling = false;
            DestroyJoint(currentJoint);
        }
    }
}