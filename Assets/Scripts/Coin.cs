using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    public void GetCollected()
    {
        Destroy(gameObject);
    }
}
