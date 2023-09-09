using UnityEngine;

public class RangeHelper : MonoBehaviour
{
    private CharacterSO characterSettings;
    private RectTransform rectTransform;

    private void Awake()
    {
        characterSettings = GetComponentInParent<CharacterSettings>().GetSettings();
        rectTransform = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        float range = characterSettings.firingRange;
        rectTransform.sizeDelta = new Vector2(range * 2, range * 2);
    }
}
