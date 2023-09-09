using UnityEngine;

public class CharacterSettings : MonoBehaviour
{
    [SerializeField]
    private CharacterSO characterSO;

    public CharacterSO GetSettings()
    {
        return characterSO;
    }
}
