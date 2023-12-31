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
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        animator = GetComponentInParent<Animator>();
        health = characterSettings.maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GetKilled();
        }
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

    public void GetKilled()
    {
        if (animator != null) animator.SetBool("IsDead", true);
        OnPlayerDied?.Invoke();
    }
}
