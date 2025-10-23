using UnityEngine;
using UnityEngine.InputSystem;

public class inputReader : MonoBehaviour
{
  public float horizontalMove => Gamepad.current.leftStick.ReadValue().x;
}
