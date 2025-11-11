using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{
    
    private playerMove playerMove;
    private inputReader inputReader;
    private ContextReader context;
    private groundedType contextGround;
    
    private JointManager jointManager;
    private GearAction gearAction;
    
    

    private void Start()
    {
        playerMove = gameObject.GetComponent<playerMove>();
        inputReader = gameObject.GetComponent<inputReader>();
        context = gameObject.GetComponent<ContextReader>();
        jointManager = gameObject.GetComponent<JointManager>();
        gearAction = FindAnyObjectByType<GearAction>();
    }
        

    public void InterpretMoove(actionState actionType)
    {
        
        switch (actionType,context.groundedType)
        {
            //Grounded
            case (actionState.pressed,groundedType.IsGrounded):
                playerMove.PressedMove(inputReader.horizontalMove);
                break;
            case (actionState.sustained,groundedType.IsGrounded):
                playerMove.SustainMove(inputReader.horizontalMove);
                
                break;
            case (actionState.released,groundedType.IsGrounded):
                playerMove.ReleaseMove();
                
                //OnGear
                break;
            case (actionState.pressed,groundedType.IsOnGear):
                playerMove.GearPressedMove(inputReader.horizontalMove,jointManager);
                gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z);
                
                break;
            case (actionState.sustained,groundedType.IsOnGear):
                playerMove.GearSustainedMove(inputReader.horizontalMove,jointManager);
                gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z);
                break;
            case (actionState.released,groundedType.IsOnGear):
                
                
                break;
            
            //Airborne
            case (actionState.pressed,groundedType.Airborn):
                playerMove.AirPressedMove(inputReader.horizontalMove);
                break;
            case (actionState.sustained,groundedType.Airborn):
                playerMove.AirSustainMove(inputReader.horizontalMove);
                
                break;
            case (actionState.released,groundedType.Airborn):
                playerMove.AirReleaseMove();
                
                break;
            case (actionState.nothing, groundedType.Airborn):
                playerMove.ApplyAirDecel();

                break;
            
        }
        
        }


    
    public void InterpretJump(actionState actionType)
    {
        
        switch (actionType, context.groundedType)
        {
            //OnGround
            case (actionState.pressed, groundedType.IsGrounded):
                
                playerMove.PresedJump();
                break;
            case (actionState.sustained, groundedType.IsGrounded):
                playerMove.SustainJump();

                break;
            case (actionState.released, groundedType.IsGrounded):
                playerMove.ReleaseJump();

                break;
            
           
            //OnGear
            case (actionState.pressed, groundedType.IsOnGear):
                jointManager.DestroyJoint();
                playerMove.PresedJump();
                break;
            case (actionState.sustained, groundedType.IsOnGear):
                playerMove.SustainJump();

                break;
            case (actionState.released, groundedType.IsOnGear):
                playerMove.ReleaseJump();

                break;

        }
    }
}
