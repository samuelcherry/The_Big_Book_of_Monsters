using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public PlayerData playerData;
    public EnemyData enemyData;
    public SlotUpgradeData slotUpgradeData;
    public PrestigeData prestigeData;
    public SkillPointData skillPointData;
    public AdventureData adventureData;
    public BestiaryData bestiaryData;
    public AlchemyData alchemyData;
}

[Serializable]
public class PlayerData
{
    public int level;
    public float currentXp;
    public int atkMetalCount, defMetalCount, hpMetalCount;
    public int role, roleChoosen;
}

[Serializable]
public class EnemyData
{
    public float goldAmt;
    public List<AdventureData> adventures;
}

[Serializable]
public class SlotUpgradeData
{
    public List<int> slotLevels = new List<int>();
    public int autoBuyerLevel;
}

[Serializable]
public class PrestigeData
{
    public float baseXP, prestigeMulti;
}

[Serializable]
public class SkillPointData
{
    public List<RoleData> roles;
}

[Serializable]
public class RoleData
{
    public int roleUnlocked;
    public List<UpgradeData> upgrades;
}

[Serializable]
public class UpgradeData
{
    public float metalCount;
    public bool unlocked, purchased, blocked;
}

[Serializable]
public class AdventureData
{
    public int isCompleted, prevCompleted;
    public int tempAdventureNumber;
}

[Serializable]
public class BestiaryData
{
    public List<int> defeatedEntries = new List<int>();
}

[Serializable]
public class AlchemyData
{
    public int alchAutoBuyerLevel;
    public List<AlchemyBarData> alchemyBars;
    public List<int> potionAmounts;
}

[Serializable]
public class AlchemyBarData
{
    public float alchLvl, alchXP, alchMaxXp, rwd, limit, totalTime, baseTime;
}
