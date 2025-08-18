using UnityEngine;
using System.Collections;

public class InitializeUpgradeItems : MonoBehaviour
{
    [SerializeField] private UpgradeDescription[] upgradeItems;

    private IEnumerator Start()
    {
        UpgradesPanel upgradesPanel = FindObjectOfType<UpgradesPanel>();
        // Wait for one frame to ensure UpgradesPanel.Start() has run
        yield return null;

        foreach (var upgradeItem in upgradeItems)
        {
            if (upgradeItem != null)
            {
                upgradesPanel.AddItemToUpgradeInventory(upgradeItem, 1);
            }
            else
            {
                Debug.LogWarning("Upgrade item is null, skipping initialization.");
            }
        }
    }
}
