using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum actionState
{
    pressed,
    sustained,
    released,
    nothing
    
}

public class inputReader : MonoBehaviour
{
    public InputInterpreter interpreter;

    private InputAction horizontalMoveAction;
    private InputAction jumMoveAction;
   

    public Vector3 horizontalMove;
   

    

    private void Start()
    {
        interpreter = gameObject.GetComponent<InputInterpreter>();
        horizontalMoveAction = InputSystem.actions.FindAction("Move");
        jumMoveAction = InputSystem.actions.FindAction("Jump");
       
    }

    private void Update()
    {
       
        CheckHorizontal();
        CheckJump();
    }
    private void CheckHorizontal()
    {
        
        Vector2 readHorizontalMoove = horizontalMoveAction.ReadValue<Vector2>();
        horizontalMove = new Vector3(readHorizontalMoove.x, 0, readHorizontalMoove.y);
       
        
        if (horizontalMoveAction.WasPressedThisFrame())
        {
            interpreter.InterpretMoove(actionState.pressed);
        }
        else if (horizontalMoveAction.IsInProgress())
        {
           
            interpreter.InterpretMoove(actionState.sustained);

        }
        else if (horizontalMoveAction.WasReleasedThisFrame())
        {
            
            interpreter.InterpretMoove(actionState.released);

        }
        else 
        {
            interpreter.InterpretMoove(actionState.nothing);
        }
    }

    private void CheckJump()
    {
        
        
        if (jumMoveAction.WasPressedThisFrame())
        {

            interpreter.InterpretJump(actionState.pressed);
        }
        else if (jumMoveAction.IsInProgress())
        {
           
            interpreter.InterpretJump(actionState.sustained);

        }
        else if (jumMoveAction.WasReleasedThisFrame())
        {
            
            interpreter.InterpretJump(actionState.released);

        }
        
    }

    
}