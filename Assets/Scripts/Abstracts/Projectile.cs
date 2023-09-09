using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public abstract class Projectile : MonoBehaviour
{
    [HideInInspector]
    public Vector3 projectileDirection;
    [HideInInspector]
    public int projectileDamage;
    [HideInInspector]
    public float projectileSpeed;
    [HideInInspector]
    public float projectileLifeTime;
    [HideInInspector]
    public List<string> projectileTargets;

    protected Rigidbody projectileRigidbody;

    protected virtual void Awake()
    {
        projectileDirection = Vector3.forward;
        projectileDamage = 100;
        projectileSpeed = 10f;
        projectileLifeTime = 2f;
        projectileTargets = new List<string>();
        projectileTargets.Add("Enemy");

        projectileRigidbody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        StartCoroutine(DestroyAfterTime());
        projectileRigidbody.velocity = projectileDirection.normalized * projectileSpeed;
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!projectileTargets.Contains(other.tag)) return;
        if (other.CompareTag("Environment"))
        {
            Destroy(gameObject);
            return;
        }
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(projectileDamage);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }

    protected virtual IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(projectileLifeTime);
        Destroy(gameObject);
    }
}
