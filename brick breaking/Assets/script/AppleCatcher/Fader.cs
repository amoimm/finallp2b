using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour
{
    private SpriteRenderer _rend;
    private const float FadeTime = 0.5f;
    private Coroutine _runningFadeCoroutine;
    
    /// <summary>
    /// Start is called once before the first execution of Update after the MonoBehaviour is created
    /// </summary>
    void Start()
    {
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded
    /// </summary>
    public void Awake()
    {
        _rend = GetComponent<SpriteRenderer>();
        if (_rend.color.a >= 0.9f)
        {
            FadeOut();
        }
    }

    /// <summary>
    /// Fades the object in or out over a specified duration.
    /// If a fade is already running, it stops that coroutine before starting a new one.
    /// </summary>
    public void FadeIn(float duration = FadeTime)
    {
        if (_runningFadeCoroutine != null)
        {
            StopCoroutine(_runningFadeCoroutine);
        }
        _runningFadeCoroutine = StartCoroutine(FadeMe(_rend.color.a, 1, duration));
    }
    public void FadeOut(float duration = FadeTime)
    {
        if (_runningFadeCoroutine != null)
        {
            StopCoroutine(_runningFadeCoroutine);
        }
        _runningFadeCoroutine = StartCoroutine(FadeMe(1, 0, duration));
    }

    /// <summary>
    /// Coroutine to fade the object from a start alpha to an end alpha
    /// over "duration" seconds
    /// </summary>
    /// <param name="startAlpha"></param>
    /// <param name="endAlpha"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator FadeMe(float startAlpha, float endAlpha, float duration)
    {
        float step = (endAlpha - startAlpha) / duration;

        bool continueLoop = true;
        while (continueLoop)
        {
            _rend.color = new Color(1, 1, 1, _rend.color.a + (step * Time.deltaTime));
            yield return null;
            if (step > 0 && _rend.color.a >= endAlpha)
            {
                continueLoop = false;

            }
            else if (step < 0 && _rend.color.a <= endAlpha)
            {
                continueLoop = false;
            }
        }
        
    }
}
