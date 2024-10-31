using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Tooltip tooltip;
	public SlotUpgrades slotUpgrades;
	public string tooltipMessage;


    void Start()
	{
		tooltip = FindFirstObjectByType<Tooltip>();
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
		Vector3 tooltipPosition = transform.position + new Vector3(220, 0, 0);
        tooltip.ShowTooltip(tooltipMessage, tooltipPosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
		Vector3 tooltipPosition = transform.position + new Vector3(0, 6000, 0);
        tooltip.HideTooltip(tooltipPosition);
    }
}