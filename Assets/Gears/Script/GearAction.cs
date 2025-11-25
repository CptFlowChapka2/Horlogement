using System;
using UnityEngine;

public class GearAction : MonoBehaviour
{
    [SerializeField] public float transfertInitialSpeed = 1f;
    [SerializeField] public float transfertSustainSpeed = 1f;
    [SerializeField] public bool reverseTransfert = false;
    private float reverseTransfertFloat = 1;
    

    private void Start()
    {

        if (reverseTransfert)
        {
            reverseTransfertFloat = -1;
        }
        else
        {
            reverseTransfertFloat = 1;
        }
    }
    public void TransferRotation(GameObject targetGear,GameObject player,Vector3 mouv,float speedModifier) 
    {
        GearPhysRotation targetRotation;
        if (targetGear.TryGetComponent<GearPhysRotation>(out targetRotation))
        {
            Vector3 gearToPlayer = player.transform.position-targetGear.transform.position;
            Vector3 perpendicularPositive = Vector3.ProjectOnPlane(Quaternion.AngleAxis(90f,Vector3.up)*gearToPlayer,Vector3.up);
            float angle =Vector3.Angle(mouv,perpendicularPositive) ;
            if (angle <= 45f)
            {
                targetRotation.rotationSpeed += reverseTransfertFloat*-speedModifier;
            }
            else if (angle>=90f)
            {
                targetRotation.rotationSpeed += reverseTransfertFloat*speedModifier;
            }



        }
    }
    //Call A ForcedBreak event on that Gear imediatly trigger it's local Break
    public void ForceBreak(GameObject targetGear)
    {
        GearBreak targetBreak;
        if (targetGear.TryGetComponent<GearBreak>(out targetBreak))
        {
           targetBreak.ForceBreak();
        }

    }
    //Toggle ON/off the  possibility of a Break for a Gear
    public void ToggleBreak(GameObject targetGear,bool mayhaps)
    {
        GearBreak targetBreak;
        if (targetGear.TryGetComponent<GearBreak>(out targetBreak))
        {
            targetBreak.canBreak=mayhaps;
        }
    
    }

  



}
