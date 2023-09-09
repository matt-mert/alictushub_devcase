using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FiringMechanics : MonoBehaviour
{
    public delegate void OnFireDelegate();
    public static event OnFireDelegate OnFire;

    private CharacterSO characterSettings;
    private SphereCollider sphereCollider;
    private HashSet<Transform> enemiesDetected;
    private Transform closestEnemy;
    private Transform firingPoint;
    private float firingRange;
    private float firingCooldown;
    private GameObject firingPrefab;
    private List<string> targetTags;
    private int projectileDamage;
    private float projectileSpeed;
    private float projectileLifeTime;

    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        sphereCollider = GetComponent<SphereCollider>();
        enemiesDetected = new HashSet<Transform>();
        closestEnemy = null;
        firingPoint = transform.GetChild(0);
        sphereCollider.radius = firingRange;
    }

    private void Start()
    {
        firingRange = characterSettings.firingRange;
        firingCooldown = characterSettings.firingCooldown;
        firingPrefab = characterSettings.firingPrefab;
        targetTags = characterSettings.targetTags;
        projectileDamage = characterSettings.projectileDamage;
        projectileSpeed = characterSettings.projectileSpeed;
        projectileLifeTime = characterSettings.projectileLifeTime;

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
            if (enemy.GetComponent<IDamageable>().GetHealthValue() <= 0)
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
                yield return new WaitForEndOfFrame();
                continue;
            }
            GameObject current = Instantiate(firingPrefab, firingPoint.position, Quaternion.identity);
            Projectile projectile = current.GetComponent<Projectile>();

            projectile.projectileDamage = projectileDamage;
            projectile.projectileSpeed = projectileSpeed;
            projectile.projectileLifeTime = projectileLifeTime;

            Vector3 direction = (closestEnemy.position - transform.position).normalized;
            direction = new Vector3(direction.x, 0f, direction.z);
            projectile.projectileDirection = direction;

            yield return new WaitForSeconds(firingCooldown);
        }
    }
}
