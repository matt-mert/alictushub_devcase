using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject collectablePrefab;
    [SerializeField]
    private int initialAmount;
    [SerializeField]
    private float initialRange;

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
        for (int i = 0; i < initialAmount; i++)
        {
            float randomX = Random.Range(1, initialRange);
            float randomZ = Random.Range(1, initialRange);
            Vector3 position = new Vector3(randomX, 0f, randomZ);
            Instantiate(collectablePrefab, position, Quaternion.identity);
        }
    }

    private void EnemyKilledHandler(Vector3 position)
    {
        Instantiate(collectablePrefab, position, Quaternion.identity);
    }
}
