using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class ListToText : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform inventoryGrid;
    public Inventory inventory;
    public GameObject tooltip;
    public TMP_Text tooltipText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (itemPrefab == null)
        {
            itemPrefab = Resources.Load<GameObject>("itemPrefab");
            if (itemPrefab == null)
            {
                Debug.LogError("Default prefab not found in Resources folder!");
            }
        }
    }

    private Inventory.InventoryItem currentTooltipItem;

    public void PopulateText(List<Inventory.InventoryItem> itemList) //PASS SAMPLE LIST AS A PARAMETER SO THAT YOU ONLY CALL THIS FUNTION ONCE SAMPLE LIST IS LOADED
    {
        if (itemPrefab == null)
        {
            itemPrefab = Resources.Load<GameObject>("itemPrefab");
        }

        foreach (Transform child in inventoryGrid)
        {
            Destroy(child.gameObject);
        }

        List<Inventory.InventoryItem> itemsToDisplay = itemList;

        // Generate a dynamic string from the list
        for (int i = 0; i < itemList.Count; i++)
        {
            int index = i;
            GameObject newItem = Instantiate(itemPrefab, inventoryGrid);
            UnityEngine.UI.Image slotIcon = newItem.GetComponentInChildren<UnityEngine.UI.Image>();
            TMP_Text textElement = newItem.GetComponentInChildren<TMP_Text>();
            UnityEngine.UI.Button button = newItem.GetComponentInChildren<UnityEngine.UI.Button>();

            button.gameObject.SetActive(true); // Set button's GameObject to active

            if (textElement != null)
            {
                textElement.enabled = true;
                textElement.text = $"{itemsToDisplay[i].quantity}";
            }
            if (inventory == null || inventory.masterItemList == null)
            {
                Debug.LogError("Inventory or masterItemList is null. Ensure it is assigned and initialized.");
                return;
            }

            if (slotIcon != null)
            {
                Inventory.ItemDefinition itemPath = inventory.masterItemList.Find(item => item.name == itemList[index].name);
                slotIcon.sprite = itemPath.icon;
                slotIcon.enabled = true;
            }
            button.onClick.AddListener(() => inventory.OnItemClicked(itemList[index]));

            //Enter Trigger
            EventTrigger trigger = newItem.AddComponent<EventTrigger>();

            EventTrigger.Entry OnPointerEnter = new EventTrigger.Entry();
            OnPointerEnter.eventID = EventTriggerType.PointerEnter;
            OnPointerEnter.callback.AddListener((data) =>
            {
                HandlePointerEnter(itemList[index]);
            });
            trigger.triggers.Add(OnPointerEnter);

            //Exit Trigger
            EventTrigger.Entry OnPointerExit = new EventTrigger.Entry();
            OnPointerExit.eventID = EventTriggerType.PointerExit;
            OnPointerExit.callback.AddListener((data) =>
            {
                HandlePointerExit();
            });
            trigger.triggers.Add(OnPointerExit);
        }


        if (currentTooltipItem != null && itemList.Contains(currentTooltipItem))
        {
            HandlePointerEnter(currentTooltipItem);
        }
        else
        {
            tooltip.SetActive(false);
        }
    }


    private void HandlePointerEnter(Inventory.InventoryItem item)
    {
        currentTooltipItem = item; // Track the current item

        Inventory.ItemDefinition existingItem = inventory.masterItemList.Find(masterItem => masterItem.name == item.name);

        tooltip.SetActive(true);

        if (existingItem != null)
        {
            tooltipText.text = $"<size=36><b>{item.name}</b></size>\n" +
                               $"<size=24>{existingItem.itemText}</size>";
        }
        else
        {
            tooltipText.text = $"<size=36><b>{item.name}</b></size>\n" +
                               "<size=24><i>Description not found.</i></size>";
        }
    }
    private void HandlePointerExit()
    {
        tooltip.SetActive(false); // Hide the tooltip when the pointer exits the icon
        currentTooltipItem = null; // Reset the current tooltip item
    }
}

