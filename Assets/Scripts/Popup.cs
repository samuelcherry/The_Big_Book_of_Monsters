using UnityEngine;
using TMPro;
using System.Collections;

public class Popup : MonoBehaviour
{
    public TMP_Text text;
    public float fadeDuration = 0.5f;
    public float visibleDuration = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (text != null)
        {
            Color color = text.color;
            text.color = new Color(color.r, color.g, color.b, 0);
        }

    }

    public void FadeText()
    {
        if (text != null)
        {
            StartCoroutine(FadeInAndOut());
        }
    }

    private IEnumerator FadeInAndOut()
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(visibleDuration);

        elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(1 - (elapsed / fadeDuration));
            SetTextAlpha(alpha);
            yield return null;
        }
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = text.color;
        text.color = new Color(color.r, color.g, color.b, alpha);
    }
}
