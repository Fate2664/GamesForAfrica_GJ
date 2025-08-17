using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField]  private TMP_Text moneyText; 

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Player not found! Make sure you have a GameObject tagged 'Player' with PlayerStats component");
        }
    }
    private void Update()
    {
        UpdateMoneyDisplay();
    }
    public void UpdateMoneyDisplay()
    {
        if (playerStats != null && moneyText != null)
        {
            moneyText.text = $"{playerStats.money}$";
        }
    }
    public void BuyUpgrade(UpgradeData upgrade)
    {
        if (upgrade.cost <= playerStats.money)
        {
            playerStats.UpgradeStat(upgrade.stat);
            playerStats.SpendMoney(upgrade.cost);
            UpdateMoneyDisplay();
            Debug.Log($"Purchased{upgrade.stat}");

        }
        else
            Debug.Log("Can't afford!");
    }
}

