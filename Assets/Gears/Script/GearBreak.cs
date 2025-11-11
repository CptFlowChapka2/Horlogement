using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GearBreak : MonoBehaviour
{
    [SerializeField] private float speedDecrease; //MUST BE POSSITIVE. Value added or substracted to the Gear
    [SerializeField] private mode mode; //See Enum Mode Below .

    [Header("Parameters")] [SerializeField]
    private float maxBreakSpeed = 120f;

    [SerializeField] private float minBreakSpeed = 15f;

    //parameter for random mode
    [SerializeField] private float random_RangeMin = 5f;
    [SerializeField] private float random_RangeMax = 30f; //the min and max for the Random.range
    [SerializeField] private bool random_reRandomOnBreak; //if the Random intervall must be reroll on each Break

    [SerializeField] private bool fullStop; //when true set gear.rotationSpeed to 0
    [SerializeField] private float delay_delay = 5f; //the delay for the delay mode .delay

    private float cooldown;
    private float initialCooldown;

    private GearPhysRotation thisRotation;
    private float directionModifiyer;
    [SerializeField] private bool canInvert = false;

    public bool canBreak; //if the Break behavior work

    private void Start()
    {
        gameObject.TryGetComponent<GearPhysRotation>(out thisRotation); //recover this gameObject GearRotation

        if (thisRotation.rotationSpeed.Equals(MathF.Abs(thisRotation.rotationSpeed)))
        {
            directionModifiyer = 1f;
        }
        else
        {
            directionModifiyer = -1f;
        }

        if (mode == mode.Random) // On random mode will trigger a Break event after a fixed* ammount of time
        {
            cooldown = Random.Range(random_RangeMin, random_RangeMax);


        }
        else if (mode == mode.Delay) //will trigger a Break after a fixed Delay
        {
            cooldown = delay_delay;


        }
        else //the Continius mode , reduce speed every frame
        {
            cooldown = 0f;
        }

        initialCooldown = cooldown; //cache in the initial delay
    }

    private void Update()
    {
        if (canBreak)
        {
            cooldown -= Time.deltaTime;
            Break();
        }


    }

    private void Break()
    {
        //Great Wall of If // all of them reset cooldown inside & check for rotationspeed to know if you add or substract
        //todo: use switch case
        if (mode == mode.Random)
        {
            if (cooldown <= 0f)
            {
                if (random_reRandomOnBreak)
                {
                    cooldown = Random.Range(random_RangeMin, random_RangeMax);
                }
                else
                {
                    cooldown = initialCooldown;
                }

                ModifyRotationSpeed();


            }


        }
        else if (mode == mode.Delay)
        {
            if (cooldown <= 0f)
            {
                cooldown = initialCooldown;
                ModifyRotationSpeed();
            }


        }
        else if (mode == mode.Continius)
        {
            ModifyRotationSpeed();

        }
    }
    public void ForceBreak() //WRATH OF ZEUS !!
    {
        Break();
    }


    private void ModifyRotationSpeed()
    {
        if (fullStop)
        {
            thisRotation.rotationSpeed = 0f;
        }
        else
        {


            if (canInvert)
            {
                if (MathF.Abs(thisRotation.rotationSpeed) <= maxBreakSpeed)
                {
                    thisRotation.rotationSpeed -= speedDecrease * directionModifiyer;
                }
                else
                {
                    thisRotation.rotationSpeed = maxBreakSpeed*(directionModifiyer*-1);
                }

            }
            else if (CheckOrignalDirection()&&MathF.Abs(thisRotation.rotationSpeed)>=minBreakSpeed)
            {
                
                thisRotation.rotationSpeed -= speedDecrease * directionModifiyer;
            }
            else
            {
                thisRotation.rotationSpeed = minBreakSpeed;
            }


        }
    }

    private bool CheckOrignalDirection()
    {
        bool dir = directionModifiyer.Equals(thisRotation.rotationSpeed / MathF.Abs( thisRotation.rotationSpeed));

        return dir;
    }
}

public enum mode //make the little multichoice button in editor feel nice 
{
    Random,
    Continius,
    Delay,
}