using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Transform buffGrid;
    public GameObject buffPrefab;
    public bool isOn;



    public List<GameObject> activeBuffs = new();
    public List<string> activeBuffnames = new();

    public void AddBuff(string buffName, Sprite buffIcon, float duration)
    {
        if (activeBuffnames.Contains(buffName))
        {
            Debug.LogError($"Buff '{buffName}' is already active and cannot be added again.");
            Debug.Log(activeBuffnames.Count);
            return;
        }

        Debug.Log($"Adding buff: {buffName}");

        GameObject buffObject = Instantiate(buffPrefab, buffGrid);

        Image iconImage = buffObject.transform.Find("icon").GetComponent<Image>();
        TMP_Text durationText = buffObject.transform.Find("duration").GetComponent<TMP_Text>();
        iconImage.sprite = buffIcon;
        durationText.text = $"{duration}";

        activeBuffs.Add(buffObject);
        activeBuffnames.Add(buffName);

        Debug.Log($"Active Buff Names After Addition: {string.Join(", ", activeBuffnames)}");

        StartCoroutine(HandleBuffDuration(buffName, buffObject, duration));
    }

    private IEnumerator HandleBuffDuration(string buffName, GameObject buffObject, float duration)
    {
        Debug.Log($"Started duration coroutine for buff: {buffName}");

        TMP_Text durationText = buffObject.transform.Find("duration").GetComponent<TMP_Text>();

        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration -= 1f;
            durationText.text = $"{duration}";
        }

        Debug.Log($"Buff '{buffName}' has ended. Cleaning up.");

        activeBuffs.Remove(buffObject);
        if (activeBuffnames.Contains(buffName))
        {
            activeBuffnames.Remove(buffName);
            Debug.Log($"Removed buff: {buffName} from activeBuffnames");
            Debug.Log($"Active Buff Names After Removal: {string.Join(", ", activeBuffnames)}");
        }

        Destroy(buffObject);

        playerStats.UpdateStats();
        playerStats.UpdateStatText();
    }


}