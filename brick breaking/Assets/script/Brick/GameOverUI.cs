using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void RestartGame()
    {
        GameManager.Instance.Restart();
    }
    public void QuitGame()
    {
        GameManager.Instance.Quit();
    }

}