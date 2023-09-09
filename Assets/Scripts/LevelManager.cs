using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public static int CoinsAmount { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        PickupMechanics.OnPickup += PickupHandler;
    }

    private void OnDisable()
    {
        PickupMechanics.OnPickup -= PickupHandler;
    }

    private void Start()
    {
        CoinsAmount = PlayerPrefs.GetInt("COIN_AMOUNT", 0);
    }

    private void PickupHandler()
    {
        CoinsAmount++;
        PlayerPrefs.SetInt("COIN_AMOUNT", CoinsAmount);
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
