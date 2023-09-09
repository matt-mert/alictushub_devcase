using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject collectablePrefab;

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += EnemyKilledHandler;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= EnemyKilledHandler;
    }

    private void Start()
    {
        
    }

    private void EnemyKilledHandler(Vector3 position)
    {
        Instantiate(collectablePrefab, position, Quaternion.identity);
    }
}
