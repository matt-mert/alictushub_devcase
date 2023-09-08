using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FiringMechanics : MonoBehaviour
{
    [SerializeField]
    private float firingRange;
    [SerializeField]
    private float firingCooldown;
    [SerializeField]
    private GameObject firingProjectile;
    [SerializeField]
    private List<string> targetTags;

    private HashSet<Transform> enemiesDetected;
    private Transform closestEnemy;
    private SphereCollider sphereCollider;
    private Transform firingPoint;
    private WaitForEndOfFrame waitForEndOfFrame;
    private WaitForSeconds waitForSeconds;

    private void Awake()
    {
        enemiesDetected = new HashSet<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
        firingPoint = transform.GetChild(0);
        sphereCollider.radius = firingRange;
        waitForEndOfFrame = new WaitForEndOfFrame();
        waitForSeconds = new WaitForSeconds(firingCooldown);
    }

    private void Start()
    {
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
            // TODO: if isDead, continue

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


            yield return waitForSeconds;
        }
    }
}
