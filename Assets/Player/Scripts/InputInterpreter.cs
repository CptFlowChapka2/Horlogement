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
        

    public void InterpretMoove(string actionType)
    {
        
        switch (actionType,context.groundedType)
        {
            case ("pressed",groundedType.IsGrounded):
                playerMove.PressedMove(inputReader.horizontalMove);
                break;
            case ("sustained",groundedType.IsGrounded):
                playerMove.SustainMove(inputReader.horizontalMove);
                
                break;
            case ("released",groundedType.IsGrounded):
                playerMove.ReleaseMove();
                
                break;
            case ("pressed",groundedType.IsOnGear):
                playerMove.GearPressedMove(inputReader.horizontalMove,jointManager);
                gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z);
                
                break;
            case ("sustained",groundedType.IsOnGear):
                playerMove.GearSustainedMove(inputReader.horizontalMove,jointManager);
                gearAction.TransferRotation(context.currentGear,inputReader.horizontalMove.z);
                break;
            case ("released",groundedType.IsOnGear):
                
                
                break;
            
            case ("pressed",groundedType.Airborn):
                
                break;
        }


    }
    public void InterpretJump(string actionType)
    {
        
        switch (actionType, context.groundedType)
        {
            //OnGround
            case ("pressed", groundedType.IsGrounded):
                
                playerMove.PresedJump();
                break;
            case ("sustained", groundedType.IsGrounded):
                playerMove.SustainJump();

                break;
            case ("released", groundedType.IsGrounded):
                playerMove.ReleaseJump();

                break;
           
            //OnGear
            case ("pressed", groundedType.IsOnGear):
                jointManager.DestroyJoint();
                playerMove.PresedJump();
                break;
            case ("sustained", groundedType.IsOnGear):
                playerMove.SustainJump();

                break;
            case ("released", groundedType.IsOnGear):
                playerMove.ReleaseJump();

                break;

        }
    }
}
