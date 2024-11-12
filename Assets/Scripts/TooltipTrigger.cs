using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Tooltip tooltip;
    public SlotUpgrades slotUpgrades;
    public Upgrades upgrades;
    public string upgradeDescription;
    public string tooltipMessage;
    public int upgradeIndex;


    void Start()
    {
        tooltip = FindFirstObjectByType<Tooltip>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

        Vector3 tooltipPosition = transform.position + new Vector3(220, 0, 0);

        for (int r = 0; r < upgrades.roles.Length; r++)
        {
            var upgrade = upgrades.roles[r].upgrades[upgradeIndex];

            tooltipMessage =
            $"{upgradeDescription}\n" +
            "Skill Points\n" +
            $" {upgrade.metalCount} / {upgrade.metalMax}";
        }


        tooltip.ShowTooltip(tooltipMessage, tooltipPosition);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Vector3 tooltipPosition = transform.position + new Vector3(0, 6000, 0);
        tooltip.HideTooltip(tooltipPosition);
    }
}