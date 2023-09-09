using UnityEngine;
using TMPro;

public class InGameUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI enemiesText;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject retryPanel;

    private FloatingJoystick joystick;
    private int enemiesKilled;

    private void Awake()
    {
        joystick = FindObjectOfType<FloatingJoystick>();
    }

    private void OnEnable()
    {
        EnemyHealth.OnEnemyKilled += EnemyKilledHandler;
        PlayerHealth.OnPlayerDied += PlayerDiedHandler;
        PickupMechanics.OnPickup += PickupHandler;
    }

    private void OnDisable()
    {
        EnemyHealth.OnEnemyKilled -= EnemyKilledHandler;
        PlayerHealth.OnPlayerDied -= PlayerDiedHandler;
        PickupMechanics.OnPickup -= PickupHandler;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        retryPanel.SetActive(false);
        enemiesKilled = 0;
        enemiesText.text = enemiesKilled.ToString();
        coinsText.text = LevelManager.CoinsAmount.ToString();
    }

    private void EnemyKilledHandler(Vector3 position)
    {
        enemiesKilled++;
        enemiesText.text = enemiesKilled.ToString();
    }

    private void PlayerDiedHandler()
    {
        Time.timeScale = 0;
        retryPanel.SetActive(true);
    }

    private void PickupHandler()
    {
        coinsText.text = LevelManager.CoinsAmount.ToString();
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        joystick.enabled = false;
        pausePanel.SetActive(true);
    }

    public void ResumeButton()
    {
        pausePanel.SetActive(false);
        joystick.enabled = true;
        Time.timeScale = 1;
    }

    public void RestartButton()
    {
        LevelManager.RestartLevel();
    }

    public void MainMenuButton()
    {
        LevelManager.LoadMainMenu();
    }
}
