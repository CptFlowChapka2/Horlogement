using System;
using UnityEngine;

public class JointManager : MonoBehaviour
{

    public FixedJoint currentJoint;
    public float breakForce;


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    

    public void CreateJoint(GameObject gear)
    {
        currentJoint = gameObject.AddComponent<FixedJoint>();
        currentJoint.connectedBody = gear.GetComponent<Rigidbody>();
        currentJoint.enableCollision = true;
        currentJoint.autoConfigureConnectedAnchor = true;
        currentJoint.enablePreprocessing = false;

        currentJoint.breakForce = breakForce;
        
        

    }

    public void MooveJoint(Vector3 moove)
    {
        if (currentJoint.autoConfigureConnectedAnchor && currentJoint is not null)
        {
            Vector3 cacheAnchor = currentJoint.connectedAnchor;
                    
                    currentJoint.autoConfigureConnectedAnchor = false;
                    currentJoint.connectedAnchor = cacheAnchor;
        }
        
        currentJoint.anchor += moove;
    }

    public void DestroyJoint()
    {
        if (currentJoint is not null)
        {
            Destroy(currentJoint);
        }
    }


}