
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    [System.Serializable]
    public class Entry
    {
        public int IsDefeated;
        public Sprite EntryLocked;
        public Sprite EntryUnlocked;
        public GameObject EntrySlot;
        public int EntryId;
    }
    public Entry[] entry = new Entry[20];
    void Awake()
    {
        for (int i = 0; i < entry.Length; i++)
        {
            entry[i].EntryId = i;
            entry[i].IsDefeated = 0;
        }
    }



    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < entry.Length; i++)
        {
            SpriteRenderer spriteRenderer = entry[i].EntrySlot.GetComponent<SpriteRenderer>();
            if (entry[i].IsDefeated == 0)
            {
                spriteRenderer.sprite = entry[i].EntryLocked;
            }
            else if (entry[i].IsDefeated == 1)
            {
                spriteRenderer.sprite = entry[i].EntryUnlocked;
            }
        }
    }
}
