using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{
    
    private playerMove playerMove;
    private inputReader inputReader;
    private ContextReader context;
    private groundedType contextGround;
    
    private GearAction gearAction;
    private CameraManager cameraManager;
    
    

    private void Start()
    {
        playerMove = gameObject.GetComponent<playerMove>();
        inputReader = gameObject.GetComponent<inputReader>();
        context = gameObject.GetComponent<ContextReader>();
        gearAction = FindAnyObjectByType<GearAction>();
        cameraManager = FindAnyObjectByType<CameraManager>();
    }
        

    public void InterpretMoove(actionState actionType)
    {
        
        switch (actionType,context.groundedType)
        {
                
                //OnGear
            case (actionState.pressed,groundedType.IsOnGear):
               playerMove.GearPressedMove(inputReader.horizontalMove);
               //gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z,gearAction.transfertInitialSpeed);
                
                break;
            case (actionState.sustained,groundedType.IsOnGear):
               playerMove.GearSustainedMove(inputReader.horizontalMove);
               
                //gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z,gearAction.transfertSustainSpeed);
                break;
            case (actionState.released,groundedType.IsOnGear):
                playerMove.GearReleasedMove(inputReader.horizontalMove);
                
                break;
            case (actionState.nothing,groundedType.IsOnGear):
                playerMove.GearNothingMove();
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
            
           
            //OnGear
            case (actionState.pressed, groundedType.IsOnGear):
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
