using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void StartGameButton()
    {
        LevelManager.LoadGame();
    }
}
