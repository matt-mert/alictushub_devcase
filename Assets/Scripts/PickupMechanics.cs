using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupMechanics : MonoBehaviour
{
    public delegate void OnPickupDelegate();
    public static event OnPickupDelegate OnPickup;

    private CharacterSO characterSettings;
    private SphereCollider sphereCollider;
    private float pickupRange;

    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        pickupRange = characterSettings.pickupRange;
        sphereCollider.radius = pickupRange;
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
