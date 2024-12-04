using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AlchTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject alchTooltip;
    public TMP_Text alchTooltipText;

    [TextArea(3, 10)]

    public string tooltipMessage = "Default message";

    private void Start()
    {
        alchTooltip.SetActive(false);

        CanvasGroup canvasGroup = alchTooltip.GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowTooltip();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        HideTooltip();
    }

    private void ShowTooltip()
    {
        alchTooltip.SetActive(true);

        alchTooltipText.text = tooltipMessage;

    }

    private void HideTooltip()
    {
        alchTooltip.SetActive(false);
    }

}