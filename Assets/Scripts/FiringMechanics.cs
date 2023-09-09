using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FiringMechanics : MonoBehaviour
{
    [Header("CharacterSettings will overwrite.")]

    [SerializeField]
    [Range(1f, 30f)]
    private float firingRange;
    [SerializeField]
    [Range(0.1f, 2f)]
    private float firingCooldown;
    [SerializeField]
    private GameObject firingPrefab;
    [SerializeField]
    private List<string> targetTags;
    [SerializeField]
    [Range(1, 100)]
    private int projectileDamage;
    [SerializeField]
    [Range(1f, 30f)]
    private float projectileSpeed;
    [SerializeField]
    [Range(1f, 30f)]
    private float projectileLifeTime;

    private CharacterSO characterSettings;
    private SphereCollider sphereCollider;
    private HashSet<Transform> enemiesDetected;
    private Transform closestEnemy;
    private Transform firingPoint;
    private WaitForEndOfFrame waitForEndOfFrame;
    private WaitForSeconds waitForSeconds;


    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        sphereCollider = GetComponent<SphereCollider>();
        enemiesDetected = new HashSet<Transform>();
        closestEnemy = null;
        firingPoint = transform.GetChild(0);
        sphereCollider.radius = firingRange;
        waitForEndOfFrame = new WaitForEndOfFrame();
        waitForSeconds = new WaitForSeconds(firingCooldown);
    }

    private void Start()
    {
        if (characterSettings != null)
        {
            firingRange = characterSettings.firingRange;
            firingCooldown = characterSettings.firingCooldown;
            firingPrefab = characterSettings.firingPrefab;
            targetTags = characterSettings.targetTags;
            projectileDamage = characterSettings.projectileDamage;
            projectileSpeed = characterSettings.projectileSpeed;
            projectileLifeTime = characterSettings.projectileLifeTime;
        }
        else
        {
            Debug.LogWarning("CharacterSettings could not be found. Using serialized values for FiringMechanics.");
        }

        sphereCollider.radius = firingRange;
        StartCoroutine(FiringCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!targetTags.Contains(other.tag)) return;
        enemiesDetected.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!targetTags.Contains(other.tag)) return;
        enemiesDetected.Remove(other.transform);
    }

    private void Update()
    {
        if (enemiesDetected.Count == 0)
        {
            closestEnemy = null;
            return;
        }

        foreach (Transform enemy in enemiesDetected)
        {
            if (enemy.GetComponent<IDamageable>().Health <= 0)
            {
                enemiesDetected.Remove(enemy);
                continue;
            }

            if (closestEnemy == null)
            {
                closestEnemy = enemy;
                continue;
            }

            float previousDist = Vector3.Distance(closestEnemy.position, transform.position);
            float currentDist = Vector3.Distance(enemy.position, transform.position);

            if (currentDist < previousDist)
            {
                closestEnemy = enemy;
            }
        }
    }

    private IEnumerator FiringCoroutine()
    {
        while (true)
        {
            if (closestEnemy == null)
            {
                yield return waitForEndOfFrame;
                continue;
            }
            GameObject current = Instantiate(firingPrefab, Vector3.zero, Quaternion.identity);
            Projectile projectile = current.GetComponent<Projectile>();
            yield return waitForSeconds;
        }
    }
}
