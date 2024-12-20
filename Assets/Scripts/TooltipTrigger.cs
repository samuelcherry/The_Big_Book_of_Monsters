using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tooltip tooltip;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public string upgradeDescription;
    string tooltipMessage;
    public int roleIndex;
    public int upgradeIndex;



    void Start()
    {
        tooltip = FindFirstObjectByType<Tooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Vector3 tooltipPosition = transform.position + new Vector3(220, 0, 0);

        var upgrade = upgrades.roles[roleIndex].upgrades[upgradeIndex];
        tooltipMessage =
        $"{upgradeDescription}\n" +
        "Skill Points\n" +
        $" {upgrade.metalCount} / {upgrade.metalMax}";

        tooltip.ShowTooltip(tooltipMessage, tooltipPosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 tooltipPosition = transform.position + new Vector3(0, 6000, 0);
        tooltip.HideTooltip(tooltipPosition);
    }
}