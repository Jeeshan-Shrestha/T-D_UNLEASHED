using UnityEngine;
using TMPro;
using System.Collections;

public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float fadeDuration = 1f;
    public float visibleTime = 1.5f;
    public float blinkSpeed = 0.15f;   // Time between blinks

    public GameObject buttonObject;
    void Start()
    {
    }

    public IEnumerator FadeRoutine()
    {
        Color c = text.color;
        c.a = 0;
        text.color = c;
        buttonObject.SetActive(false);

        // Fade in
        yield return StartCoroutine(Fade(0, 1));

        // Blink for 3 seconds
        yield return StartCoroutine(Blink(visibleTime));

        // Fade out
        yield return StartCoroutine(Fade(1, 0));

        buttonObject.SetActive(true);
    }

    IEnumerator Fade(float start, float end)
    {
        float t = 0;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;

            Color c = text.color;
            c.a = Mathf.Lerp(start, end, t / fadeDuration);
            text.color = c;

            yield return null;
        }
    }

    IEnumerator Blink(float duration)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            text.enabled = !text.enabled;   // Toggle visibility

            yield return new WaitForSeconds(blinkSpeed);
            elapsed += blinkSpeed;
        }

        text.enabled = true; // Ensure it's visible before fading out
    }
}