using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeData
{
    public string displayName;
    public Sprite icon;
    [TextArea] public string description;
    public int cost;

    public enum StatType {attackDamage, attackRange,attackSpeed, bulletCount, bulletSpeed,spreadAngle, bulletSize, moveSpeed }
    public StatType statType;
    public float increaseAmount;
}