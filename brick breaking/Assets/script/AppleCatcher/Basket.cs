using UnityEngine;
using TMPro;

public class Basket : MonoBehaviour
{
    [SerializeField] float _speed = 12.5f;
    [SerializeField] TextMeshPro _scoreText;
    private int _score;
    private AudioSource _audio;
    [SerializeField] private AudioClip normalClip;
    [SerializeField] private AudioClip goldenClip;
    [SerializeField] private AudioClip rottenClip;
    private Animator ref_animator;

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    private void Awake()
    {
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.loop = false;
        _audio.playOnAwake = false;
        _audio.volume = 0.5f;
    }
    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// This is where we initialize components
    /// </summary>
    void Start()
    {
        ref_animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Update is called once per frame, this is where the basket movement is handled
    /// </summary>
    void Update()
    {
        float speedFromInputThisFrame = 0f;

        // Check for input and move the basket accordingly
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < 8f)
        {
            speedFromInputThisFrame = _speed;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > -8f)
        {
            speedFromInputThisFrame = -_speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            Time.timeScale = 1.0f;
        }

        transform.Translate(Vector3.right * speedFromInputThisFrame * Time.deltaTime);
        // Update the animator's speed parameter based on input
        ref_animator.SetFloat("Speed", speedFromInputThisFrame);
    }

    /// <summary>
    /// Called when the basket collides with an apple
    /// This method checks the type of apple and updates the score accordingly
    /// </summary>
    void OnCollisionEnter2D(Collision2D col){
        Apple apple = col.gameObject.GetComponent<Apple>();
        if (apple != null)
        {
            // Play different sound based on the apple's points value
            switch (apple.points)
            {
                case 5:
                    _audio.clip = goldenClip;
                    break;
                case -3:
                    _audio.clip = rottenClip;
                    break;
                default:
                    _audio.clip = normalClip;
                    break;
            }

            _score += apple.points;
            // Ensure score doesn't become negative
            _score = Mathf.Max(_score, 0);
            _scoreText.text = "Score : " + _score;
            _audio.Play();
            Destroy(col.gameObject);
        }
    }
}
