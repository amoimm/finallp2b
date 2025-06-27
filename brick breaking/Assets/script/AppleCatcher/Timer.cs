using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Timer : MonoBehaviour
{
    private TextMeshPro _timerText;
    [SerializeField] private TextMeshPro _scoreText;
    [SerializeField] private Fader fader;
    private float timeRemaining = 120f;
    private bool timerIsRunning = true;
    private bool hasEnded = false;

    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// This is where we initialize components
    /// </summary>
    void Start()
    {
        _timerText = GetComponent<TextMeshPro>();
        UpdateTimerDisplay();
    }

    /// <summary>
    /// Update is called once per frame, this is where the timer logic is handled
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
        }
        if (timerIsRunning)
        {
            // If the timer is running, decrease the time remaining
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimerDisplay();

                // Change the color of the timer text to red if time remaining is less than or equal to 30 seconds
                if (timeRemaining <= 30f)
                {
                    _timerText.color = Color.red;
                }
            }
            // If the time remaining is less than or equal to 0, stop the timer and trigger the end game sequence
            else if (!hasEnded)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                hasEnded = true;
                StartCoroutine(EndGameSequence());
            }
        }
    }

    /// <summary>
    /// Updates the timer display text with the current time remaining in MM:SS format
    /// </summary>
    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        _timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }

    /// <summary>
    /// Coroutine to handle the end game sequence, displaying "Game Over" and transitioning to the
    /// Title scene after a delay
    /// </summary>
    IEnumerator EndGameSequence()
    {
        // Displays “Game Over” in the center of the screen with the score
        _timerText.text = "Game Over";
        _timerText.fontSize = 24;
        _timerText.color = new Color(105f / 255f, 105f / 255f, 105f / 255f); // Dark gray color
        // Change the disposition of the timer and score text
        _timerText.transform.position = new Vector3(0f, 2f, -2f);
        _scoreText.transform.position = new Vector3(0.5f, -1f, -2f); 

        // Wait to let the player see the "Game Over" message
        yield return new WaitForSeconds(3f);

        // Fade-in
        if (fader != null)
        {
            fader.FadeIn();
            yield return new WaitForSeconds(0.5f);
        }

        // Change scene to Title
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("AppleCatcher_Title");
    }
}