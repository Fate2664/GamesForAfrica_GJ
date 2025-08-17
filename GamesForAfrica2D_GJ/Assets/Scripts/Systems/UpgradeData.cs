using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class UpgradeData : MonoBehaviour
{
    public string displayName;
    public Sprite icon;
    [TextArea] public string description;
    public int cost;
    public PlayerStats.StatType stat;
}