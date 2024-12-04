using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tooltip;
    public TMP_Text tooltipText;
    string tooltipMessage;
    public string itemName;


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
        tooltip.SetActive(true);

        tooltipText.text = tooltipMessage;

    }

    private void HideTooltip()
    {
        tooltip.SetActive(false);
    }
}