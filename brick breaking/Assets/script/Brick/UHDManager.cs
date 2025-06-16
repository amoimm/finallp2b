using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int previousScore = -1;

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.score != previousScore)
        {
            previousScore = GameManager.Instance.score;
            UpdateScoreDisplay(previousScore);
        }
    }

    void UpdateScoreDisplay(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }
}