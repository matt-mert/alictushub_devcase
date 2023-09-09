using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class PickupMechanics : MonoBehaviour
{
    public delegate void OnPickupDelegate();
    public static event OnPickupDelegate OnPickup;

    private CharacterSO characterSettings;
    private CapsuleCollider capsuleCollider;
    private float pickupRange;

    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        pickupRange = characterSettings.pickupRange;
        capsuleCollider.radius = pickupRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.GetCollected();
            OnPickup?.Invoke();
        }
    }
}
