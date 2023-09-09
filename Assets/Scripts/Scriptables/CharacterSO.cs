using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sample Settings", menuName = "Scriptables/Character Settings")]
public class CharacterSO : ScriptableObject
{
    [Header("Movement Settings:")]

    [Range(1f, 30f)]
    public float moveSpeed;
    [Range(0.1f, 2f)]
    public float moveSmoothness;
    [Range(1f, 30f)]
    public float rotateSpeed;

    [Header("Health Settings:")]

    [Range(1, 500)]
    public int maxHealth;

    [Header("Firing Settings:")]

    [Range(1f, 30f)]
    public float firingRange;
    [Range(0.1f, 2f)]
    public float firingCooldown;
    public GameObject firingPrefab;
    public List<string> targetTags;
    [Range(1, 100)]
    public int projectileDamage;
    [Range(1f, 30f)]
    public float projectileSpeed;
    [Range(1f, 30f)]
    public float projectileLifeTime;

    [Header("Pickup Settings:")]
    [Header("Not Applicable for Enemies.")]

    [Range(1f, 30f)]
    public float pickupRange;
}
