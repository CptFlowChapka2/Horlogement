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
    public bool ignoreX = true;
    public bool ignoreMouseY = true;
     public InputInterpreter interpreter;

    private InputAction horizontalMoveAction;
    private InputAction jumMoveAction;
    private InputAction mouseMoveAction;

    public Vector3 horizontalMove;
    public Vector3 mouseMove;

    

    private void Start()
    {
        interpreter = gameObject.GetComponent<InputInterpreter>();
        horizontalMoveAction = InputSystem.actions.FindAction("Move");
        jumMoveAction = InputSystem.actions.FindAction("Jump");
        mouseMoveAction = InputSystem.actions.FindAction("Look");
    }

    private void Update()
    {
        CheckMouse();
        CheckHorizontal();
        CheckJump();
    }
    private void CheckHorizontal()
    {
        
        Vector2 readHorizontalMoove = horizontalMoveAction.ReadValue<Vector2>();
        horizontalMove = new Vector3(readHorizontalMoove.x, 0, readHorizontalMoove.y);
        if (ignoreX)
        {
            horizontalMove.x = 0;
        }
        
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

    private void CheckMouse()
    {
        Vector2 readMouseMoove = mouseMoveAction.ReadValue<Vector2>();
        mouseMove = new Vector3(readMouseMoove.x, readMouseMoove.y, 0);
        if (ignoreMouseY)
        {
            mouseMove.y = 0;
        }

        if (mouseMoveAction.triggered)
        {
           interpreter.InterpretMouse() ;
        }
    }
}