using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("CharacterSettings will overwrite.")]

    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float moveSmoothness;
    [SerializeField]
    private float rotateSpeed;

    private CharacterSO characterSettings;
    private CharacterController controller;
    private Animator animator;
    private FloatingJoystick joystick;
    private Vector3 direction;
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Vector3 zero;

    private void Awake()
    {
        characterSettings = GetComponent<CharacterSettings>().GetSettings();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void Start()
    {
        if (characterSettings != null)
        {
            moveSpeed = characterSettings.moveSpeed;
            moveSmoothness = characterSettings.moveSmoothness;
            rotateSpeed = characterSettings.rotateSpeed;
        }
        else
        {
            Debug.LogWarning("CharacterSettings could not be found. Using serialized values for PlayerController.");
        }

        direction = Vector3.zero;
        currentPos = Vector3.zero;
        zero = Vector3.zero;
        animator?.SetBool("IsMoving", false);
    }

    private void Update()
    {
        if (joystick == null)
        {
            Debug.LogError("Joystick could not be found in the scene!");
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
