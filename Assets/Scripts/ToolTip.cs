using TMPro;
using UnityEngine;

public class Tooltip : MonoBehaviour
{

    public TMP_Text tooltipText;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    public void ShowTooltip(string message, Vector3 position)
    {
        tooltipText.text = message;
        canvasGroup.transform.position = position;
        canvasGroup.alpha = 1;
    }
    public void HideTooltip(Vector3 position)
    {
        canvasGroup.transform.position = position;
        canvasGroup.alpha = 0;
    }
}
