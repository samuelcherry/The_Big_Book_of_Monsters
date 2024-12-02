using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Popup2 : MonoBehaviour
{
    public TMP_Text text;
    public Image popupBackground; // Reference to the Image component of the popup background
    public float fadeDuration = 0.5f;
    public float visibleDuration = 1f;

    private void Start()
    {
        // Initialize the alpha values to 0 (invisible)
        SetAlpha(0f);
    }

    public void FadePopup()
    {
        if (popupBackground != null && text != null)
        {
            StartCoroutine(FadeInAndOut());
        }
    }

    private IEnumerator FadeInAndOut()
    {
        // Fade in
        yield return Fade(0f, 1f, fadeDuration);

        // Stay visible for the specified duration
        yield return new WaitForSeconds(visibleDuration);

        // Fade out
        yield return Fade(1f, 0f, fadeDuration);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            SetAlpha(alpha);
            yield return null;
        }

        // Ensure final alpha is set
        SetAlpha(endAlpha);
    }

    private void SetAlpha(float alpha)
    {
        // Set the alpha for the background
        if (popupBackground != null)
        {
            Color bgColor = popupBackground.color;
            popupBackground.color = new Color(bgColor.r, bgColor.g, bgColor.b, alpha);
        }

        // Set the alpha for the text
        if (text != null)
        {
            Color textColor = text.color;
            text.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
        }
    }
}