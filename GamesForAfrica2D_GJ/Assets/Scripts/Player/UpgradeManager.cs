using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    public void BuyUpgrade(UpgradeData upgrade)
    {
        if (upgrade.cost > PlayerStats.baseMoney)
            return;
        playerStats.UpgradeStat(upgrade.stat);
        playerStats.money -= upgrade.cost;
    }

}

