using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FiringMechanics : MonoBehaviour
{
    [SerializeField]
    private float firingRange;
    [SerializeField]
    private List<string> targetTags;

    private HashSet<Transform> enemiesDetected;
    private SphereCollider sphereCollider;
    private Transform firingPoint;

    private void Awake()
    {
        enemiesDetected = new HashSet<Transform>();
        sphereCollider = GetComponent<SphereCollider>();
        firingPoint = transform.GetChild(0);
    }

    private void Start()
    {
        sphereCollider.radius = firingRange;
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
        
    }
}
