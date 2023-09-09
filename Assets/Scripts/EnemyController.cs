using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterSettings))]
public class EnemyController : MonoBehaviour
{
    private CharacterSO characterSettings;
    private CharacterController controller;
    private Animator animator;
    private Transform playerTransform;
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Vector3 direction;
    private Vector3 zero;
    private float moveSpeed;
    private float moveSmoothness;
    private float rotateSpeed;

    private EnemyState myState;

    private enum EnemyState
    {
        Running,
        Attacking,
        Dying,
    }

    private void Awake()
    {
        characterSettings = GetComponent<CharacterSettings>().GetSettings();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerTransform = FindObjectOfType<PlayerHealth>().transform;
    }

    private void Start()
    {
        moveSpeed = characterSettings.moveSpeed;
        moveSmoothness = characterSettings.moveSmoothness;
        rotateSpeed = characterSettings.rotateSpeed;
        direction = Vector3.zero;
        zero = Vector3.zero;

        myState = EnemyState.Running;
    }
}
