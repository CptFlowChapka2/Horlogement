using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pivot3rdPerson : MonoBehaviour
{
    public GameObject player;

    private InputAction cameraAction; //Datatype for all action in NewInputSystem
    private Vector3 cameraMouvement;
    public float cameraSpeed = 1f;

    private void Start()
    {
        cameraAction = InputSystem.actions.FindAction("Look");//we assigne this action kinda like get.Component
    }
    private void Update()
    {
        // cameraMouvement = cameraAction.ReadValue<Vector2>();//we recover teh V2 value of the action
        // transform.position = player.transform.position;//we maintain the pivot on the player
        //
        // Vector3 currentEuleur = transform.eulerAngles;//we remember the Initial angles
        //
        // float currentEuleurX = currentEuleur.x + cameraMouvement.y * cameraSpeed * Time.deltaTime;//we add the mouse mouvement value in the correct angles
        // float currentEuleurY = currentEuleur.y - cameraMouvement.x * cameraSpeed * Time.deltaTime;
        //
        // currentEuleur.x = currentEuleurX;//we assign the new values
        //
        // currentEuleur.y = currentEuleurY;
        // transform.eulerAngles = currentEuleur;//we update the angles
        
        
        
       
    }
}