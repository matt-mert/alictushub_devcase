using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private FloatingJoystick joystick;
    private Vector2 inputVector;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void Start()
    {
        inputVector = Vector2.zero;
    }

    private void Update()
    {
        inputVector = Vector2.up * joystick.Vertical + Vector2.right * joystick.Horizontal;
    }

    private void FixedUpdate()
    {
        
    }
}
