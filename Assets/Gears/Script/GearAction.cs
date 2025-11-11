using UnityEngine;

public class GearAction : MonoBehaviour
{
    [SerializeField] public float transfertInitialSpeed = 1f;
    [SerializeField] public float transfertSustainSpeed = 1f;
    //method to add a speedChange to an Gear //todo:Verify player direction relative to gear.
    public void TransferRotation(GameObject targetGear,float change,float speedModifier) 
    {
        GearPhysRotation targetRotation;
        if (targetGear.TryGetComponent<GearPhysRotation>(out targetRotation))
        {
            targetRotation.rotationSpeed += change*speedModifier;
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
