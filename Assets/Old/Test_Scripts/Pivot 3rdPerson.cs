using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pivot3rdPerson : MonoBehaviour
{
    public GameObject player;

    private InputAction cameraAction; //Datatype for all action in NewInputSystem
    private Vector3 cameraMouvement;
    public float cameraSpeedX = 1f;
    public float cameraSpeedY = 1f;

    public float clampMin = -0.99f;//must be negative and above -1
    public float clampMax = 0.99f;//must be positive and below 1

    public bool blackMagick = true;


    private void Start()
    {
        cameraAction = InputSystem.actions.FindAction("Look"); //we assigne this action kinda like get.Component
    }
    private void Update()
    {
        cameraMouvement = cameraAction.ReadValue<Vector2>(); //we recover teh V2 value of the action
        transform.position = player.transform.position; //we maintain the pivot on the player
        Vector3 currentEuleur = transform.eulerAngles; //we remember the Initial angles

        if (blackMagick)//Evil ass solution but smooth as all hell 
        {
            //we calculate Horizontal Mouvement First
            float currentEuleurY = currentEuleur.y + cameraMouvement.x * cameraSpeedX * Time.deltaTime;
            currentEuleur.y = currentEuleurY;
            transform.eulerAngles = currentEuleur;//applying the rotation
            
            //Calulate Vertical Mouvement
            //We take the Normalized vector pointing in the local forward and directly modify it
            transform.forward = new Vector3(transform.forward.x //keeping the same X and Z since  that taken care of Above
                //evil fucking magick : transform.forward is always normalised meaning 1 is 90° and -1 is -90° (for our usage)
                //this if we add on Y axis we point upper or downer ,as long as the value is between 0&1 insured by low CamSpeedY and DeltaTime
                , Math.Clamp(transform.forward.y + cameraMouvement.y * cameraSpeedY * Time.deltaTime, clampMin, clampMax)//we Clamp this value before 1 to avoid Jitterness
                , transform.forward.z);
        }
        else //"proper" solution but you can't clamp euleurAngles (cuz Unity LIES) so weird as all hell when looking up or down
        {
            float currentEuleurX = currentEuleur.x + cameraMouvement.y * cameraSpeedX * Time.deltaTime; //we add the mouse mouvement value in the correct angles
            float currentEuleurY = currentEuleur.y - cameraMouvement.x * cameraSpeedX * Time.deltaTime;

            currentEuleur.x = currentEuleurX; //we assign the new values

            currentEuleur.y = currentEuleurY;
            transform.eulerAngles = currentEuleur; //we update the angles
        }


    }
}