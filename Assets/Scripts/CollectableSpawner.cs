using UnityEngine;

public class CollectableSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject collectablePrefab;
    [SerializeField]
    private int initialAmount;
    [SerializeField]
    private float initialRange;

    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = FindObjectOfType<PlayerHealth>().transform;
    }

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
            float randomX = Random.Range(-initialRange, initialRange);
            float randomZ = Random.Range(-initialRange, initialRange);
            Vector3 position = new Vector3(playerTransform.position.x + randomX, 0f,
                                           playerTransform.position.z + randomZ);
            Vector3 modified = new Vector3(position.x, position.y + 2f, position.z);
            Instantiate(collectablePrefab, position, Quaternion.identity);
        }
    }

    private void EnemyKilledHandler(GameObject obj)
    {
        Vector3 modified = new Vector3(obj.transform.position.x, obj.transform.position.y + 2f, obj.transform.position.z);
        Instantiate(collectablePrefab, modified, Quaternion.identity);
    }
}
