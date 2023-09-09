using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterSettings))]
public class EnemyController : MonoBehaviour
{
    private CharacterSO characterSettings;
    private CharacterController controller;
    private EnemyHealth enemyHealth;
    private Animator animator;
    private Transform playerTransform;
    private Vector3 currentPos;
    private Quaternion currentRot;
    private Vector3 zero;
    private float moveSpeed;
    private float moveSmoothness;
    private float rotateSpeed;

    [SerializeField]
    private Transform firingPoint;

    private GameObject firingPrefab;
    private float firingRange;
    private int projectileDamage;
    private float projectileSpeed;
    private float projectileLifeTime;
    private List<string> targetTags;

    private bool attackingFlag;
    private EnemyState enemyState;

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

        firingPrefab = characterSettings.firingPrefab;
        firingRange = characterSettings.firingRange;

        projectileDamage = characterSettings.projectileDamage;
        projectileSpeed = characterSettings.projectileSpeed;
        projectileLifeTime = characterSettings.projectileLifeTime;
        targetTags = characterSettings.targetTags;
        zero = Vector3.zero;

        enemyState = EnemyState.Running;
        animator.SetBool("IsAttacking", false);
        attackingFlag = false;
    }

    private void Update()
    {
        if (enemyState == EnemyState.Running)
        {
            if (animator.GetBool("IsDead"))
            {
                enemyState = EnemyState.Dying;
                return;
            }

            if (Vector3.Distance(transform.position, playerTransform.position) < firingRange)
            {
                enemyState = EnemyState.Attacking;
                animator.SetBool("IsAttacking", true);
                attackingFlag = true;
            }
        }
        else if (enemyState == EnemyState.Attacking)
        {
            if (animator.GetBool("IsDead"))
            {
                enemyState = EnemyState.Dying;
                return;
            }

            if (attackingFlag == false)
            {
                enemyState = EnemyState.Running;
                animator.SetBool("IsAttacking", false);
            }
        }
        else if (enemyState == EnemyState.Dying)
        {
            controller.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (enemyState == EnemyState.Running)
        {
            Vector3 targetPos = (playerTransform.position - transform.position).normalized * moveSpeed * Time.fixedDeltaTime;
            currentPos = Vector3.SmoothDamp(currentPos, targetPos, ref zero, moveSmoothness);
            currentPos.Set(currentPos.x, 0f, currentPos.z);
            controller.Move(currentPos);

            Quaternion targetRot = Quaternion.LookRotation(playerTransform.position - transform.position);
            currentRot = Quaternion.Slerp(currentRot, targetRot, rotateSpeed * Time.fixedDeltaTime);
            currentRot = Quaternion.Euler(new Vector3(0f, currentRot.eulerAngles.y, 0f));
            transform.rotation = currentRot;
        }
    }

    public void ResetAttackingFlag()
    {
        attackingFlag = false;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void ThrowProjectile()
    {
        GameObject current = Instantiate(firingPrefab, firingPoint.position, Quaternion.identity);
        Projectile projectile = current.GetComponent<Projectile>();

        projectile.projectileDamage = projectileDamage;
        projectile.projectileSpeed = projectileSpeed;
        projectile.projectileLifeTime = projectileLifeTime;
        projectile.projectileTargets = targetTags;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        direction = new Vector3(direction.x, 0f, direction.z);
        projectile.projectileDirection = direction;
    }
}
