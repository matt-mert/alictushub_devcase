using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupMechanics : MonoBehaviour
{
    [Header("CharacterSettings will overwrite.")]

    [SerializeField]
    [Range(1f, 30f)]
    private float pickupRange;

    private CharacterSO characterSettings;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        if (characterSettings != null)
        {
            pickupRange = characterSettings.pickupRange;
        }
        else
        {
            Debug.LogWarning("CharacterSettings could not be found. Using serialized values for PickupMechanics.");
        }

        sphereCollider.radius = pickupRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        ICollectable collectable = other.GetComponent<ICollectable>();
        if (collectable != null)
        {
            collectable.Collect();
        }
    }
}
