using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterSettings))]
public class PlayerController : MonoBehaviour
{
    private CharacterSO characterSettings;
    private CharacterController controller;
    private Animator animator;
    private FloatingJoystick joystick;
    private Vector3 direction;
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Vector3 zero;
    private float moveSpeed;
    private float moveSmoothness;
    private float rotateSpeed;

    private void Awake()
    {
        characterSettings = GetComponent<CharacterSettings>().GetSettings();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void Start()
    {
        moveSpeed = characterSettings.moveSpeed;
        moveSmoothness = characterSettings.moveSmoothness;
        rotateSpeed = characterSettings.rotateSpeed;

        direction = Vector3.zero;
        currentPos = Vector3.zero;
        zero = Vector3.zero;
        animator?.SetBool("IsMoving", false);
    }

    private void Update()
    {
        if (joystick == null || joystick.enabled == false)
        {
            direction = Vector3.zero;
            return;
        }

        direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;

        animator?.SetBool("IsMoving", direction != Vector3.zero);
    }

    private void FixedUpdate()
    {
        Vector3 targetPos = direction * moveSpeed * Time.fixedDeltaTime;
        currentPos = Vector3.SmoothDamp(currentPos, targetPos, ref zero, moveSmoothness);
        currentPos.Set(currentPos.x, 0f, currentPos.z);
        controller.Move(currentPos);

        if (direction == Vector3.zero) return;

        Quaternion targetRot = Quaternion.LookRotation(direction);
        currentRot = Quaternion.Slerp(currentRot, targetRot, rotateSpeed * Time.fixedDeltaTime);
        currentRot = Quaternion.Euler(new Vector3(0f, currentRot.eulerAngles.y, 0f));
        transform.rotation = currentRot;
    }
}
