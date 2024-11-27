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
        GameObject buffObject = Instantiate(buffPrefab, buffGrid);

        Image iconImage = buffObject.transform.Find("icon").GetComponent<Image>();
        TMP_Text durationText = buffObject.transform.Find("duration").GetComponent<TMP_Text>();
        iconImage.sprite = buffIcon;
        durationText.text = $"{duration:F}s";

        activeBuffs.Add(buffObject);

        Debug.Log(activeBuffnames);

        StartCoroutine(HandleBuffDuration(buffName, buffObject, duration));
    }

    private IEnumerator HandleBuffDuration(string buffName, GameObject buffObject, float duration)
    {
        TMP_Text durationText = buffObject.transform.Find("duration").GetComponent<TMP_Text>();

        while (duration > 0)
        {
            yield return new WaitForSeconds(1f);
            duration -= 1f;
            durationText.text = $"{duration:F1}s";
        }
        activeBuffs.Remove(buffObject);
        activeBuffnames.Remove(buffName);
        Destroy(buffObject);
        playerStats.UpdateStats();
        playerStats.UpdateStatText();

    }

}