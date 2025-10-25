using System;
using Unity.VisualScripting;
using UnityEngine;

public class InputInterpreter : MonoBehaviour
{
    
    private playerMove playerMove;
    private inputReader inputReader;
    private ContextReader context;
    private GearAction GearAction;

    private void Start()
    {
        playerMove = gameObject.GetComponent<playerMove>();
        inputReader = gameObject.GetComponent<inputReader>();
    }
        

    public void InterpretMoove(string actionType)
    {
        switch (actionType)
        {
            case "pressed":
                playerMove.PressedMove(inputReader.horizontalMove);
                break;
            case "sustained":
                playerMove.SustainMove(inputReader.horizontalMove);
                
                break;
            case "released":
                playerMove.ReleaseMove();
                
                break;
        }


    }
}
