using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private TextMeshProUGUI enemiesText;
    [SerializeField]
    private TextMeshProUGUI coinsText;

    private int enemiesKilled;
    private int coinsCollected;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += EnemyKilledHandler;
        PickupMechanics.OnPickup += PickupHandler;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= EnemyKilledHandler;
        PickupMechanics.OnPickup -= PickupHandler;
    }

    private void Start()
    {
        enemiesKilled = 0;
        enemiesText.text = enemiesKilled.ToString();

        coinsCollected = PlayerPrefs.GetInt("COINS", 0);
        coinsText.text = coinsCollected.ToString();
    }

    private void EnemyKilledHandler()
    {
        enemiesKilled++;
        enemiesText.text = enemiesKilled.ToString();
    }

    private void PickupHandler()
    {
        coinsCollected++;
        PlayerPrefs.SetInt("COINS", coinsCollected);
        coinsText.text = coinsCollected.ToString();
    }
}
