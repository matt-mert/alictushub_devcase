using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public delegate void OnEnemyKilledDelegate();
    public static event OnEnemyKilledDelegate OnEnemyKilled;

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

        }
        else
        {
            GetKilled();
        }
    }

    private void GetKilled()
    {
        // if (animator != null) animator.SetBool("IsDead", true);
        OnEnemyKilled?.Invoke();
    }
}
