using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public delegate void OnPlayerDamagedDelegate(int updated);
    public static event OnPlayerDamagedDelegate OnPlayerDamaged;
    public delegate void OnPlayerDiedDelegate();
    public static event OnPlayerDiedDelegate OnPlayerDied;

    private int health;

    private CharacterSO characterSettings;
    private Animator animator;

    private void Awake()
    {
        characterSettings = GetComponent<CharacterSettings>().GetSettings();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        health = characterSettings.maxHealth;
    }

    public int GetHealthValue()
    {
        return health;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health > 0)
        {
            OnPlayerDamaged?.Invoke(health);
        }
        else
        {
            GetKilled();
        }
    }

    private void GetKilled()
    {
        if (animator != null) animator.SetBool("IsDead", true);
        OnPlayerDied?.Invoke();
    }
}
