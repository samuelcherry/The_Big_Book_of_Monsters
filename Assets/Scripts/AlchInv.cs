using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class AlchInv : MonoBehaviour
{
    [Serializable]
    public class AlchItem
    {
        public string itemName;
        public int quantity;
        public TMP_Text itemText;
    }

    public List<AlchItem> alchList;
    public Inventory inventory;

    public TMP_Text waterWeed;


    public void UpdateAlchInv()
    {

        if (alchList == null || inventory == null || inventory.sampleList == null)
        {
            Debug.LogError("alchList or inventory/sampleList is null. Cannot update inventory.");
            return;
        }


        for (int i = 0; i < alchList.Count; i++)
        {
            Inventory.InventoryItem existingItem = inventory.sampleList.Find(item => item.name == alchList[i].itemName);
            if (existingItem != null)
            {
                alchList[i].itemText.text = existingItem.quantity.ToString();
            }
            else
            {
                alchList[i].itemText.text = "0";
            }

        }
    }

}

