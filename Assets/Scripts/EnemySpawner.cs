using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public delegate void OnEnemySpawnedDelegate();
    public static event OnEnemySpawnedDelegate OnEnemySpawned;

    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private float enemySpawnDelay;
    [SerializeField]
    private float enemySpawnDistance;
    [SerializeField]
    private int enemiesMaxCount;
    [SerializeField]
    private float mapSizeX;
    [SerializeField]
    private float mapSizeZ;

    private Transform playerTransform;
    private int enemiesCount;

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
        StartCoroutine(EnemySpawnCoroutine());
        enemiesCount = 0;
    }

    private void EnemyKilledHandler(Vector3 position)
    {
        enemiesCount--;
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        while (true)
        {
            if (enemiesCount == enemiesMaxCount) continue;

            Vector3 selected;
            while (true)
            {
                float randomX = Random.Range(0f, mapSizeX);
                float randomZ = Random.Range(0f, mapSizeZ);
                Vector3 temp = new Vector3(randomX, 0f, randomZ);
                if (Vector3.Distance(temp, playerTransform.position) > enemySpawnDistance)
                {
                    selected = temp;
                    break;
                }
                yield return new WaitForEndOfFrame();
            }

            GameObject current = Instantiate(enemyPrefab, selected, Quaternion.identity);
            enemiesCount++;
            OnEnemySpawned?.Invoke();
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
