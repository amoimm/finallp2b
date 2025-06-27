using UnityEngine;

public class Apple : MonoBehaviour
{    
    public int points;
    private SpriteRenderer spriteRenderer;
    private AudioSource _audio;
    [SerializeField] AudioClip _audioClip;

    /// <summary>
    /// Awake is called when the script instance is being loaded, this is where we initialize components
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _audio = gameObject.AddComponent<AudioSource>();
        _audio.loop = false;
        _audio.playOnAwake = false;
        _audio.volume = 3f;
        _audio.clip = _audioClip;
    }

    public void Setup(Sprite sprite, int pointsValue)
    {
        spriteRenderer.sprite = sprite;
        points = pointsValue;
    }
    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
        
    }

    /// <summary>
    /// Update is called once per frame, this is where the falling logic is handled
    /// </summary>
    void Update()
    {
        // Check if the apple has fallen below a certain point to play the missed sound and destroy it
        if (transform.position.y < -5.5f)
        {
            if (!_audio.isPlaying && spriteRenderer.sprite.name != "RottenApple_0")
            {
                _audio.Play();
            }
            Destroy(gameObject, _audio.clip.length);
        }
    }
}
