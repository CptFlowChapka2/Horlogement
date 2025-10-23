using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class inputReader : MonoBehaviour
{
     private InputInterpreter interpreter;

    private InputAction horizontalMoveAction;

    public Vector3 horizontalMove;

    

    private void Start()
    {
        interpreter = gameObject.GetComponent<InputInterpreter>();
        horizontalMoveAction = InputSystem.actions.FindAction("Move");
    }

    private void Update()
    {
        CheckHorizontal();
    }

    private void CheckHorizontal()
    {
        Vector2 readHorizontalMoove = horizontalMoveAction.ReadValue<Vector2>();
        horizontalMove = new Vector3(readHorizontalMoove.x, 0, readHorizontalMoove.y);
        
        if (horizontalMoveAction.WasPressedThisFrame())
        {
           
            

            interpreter.InterpretMoove("pressed");
        }
        else if (horizontalMoveAction.IsInProgress())
        {
           
            interpreter.InterpretMoove("sustained");

        }
        else if (horizontalMoveAction.WasReleasedThisFrame())
        {
            
            interpreter.InterpretMoove("released");

        }
    }
}