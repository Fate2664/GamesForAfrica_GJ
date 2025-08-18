using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Initial number of enemies to spawn in the first wave.")]
    public int initialEnemiesPerWave = 3;
    public float timeBetweenSpawns = 1f;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private UpgradeManager upgradeManager;
    private int currentWave = 0;
    private bool isUpgradePanelActive = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        int enemiesToSpawn = initialEnemiesPerWave;

        while (true) // Infinite waves
        {
            Debug.Log("Current Wave: " + currentWave);
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                enemySpawner.SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            currentWave++;
            enemiesToSpawn += 2; // Increase by 2 each wave

            // Show upgrade panel and wait until it is closed
            ShowUpgradePanel();
            yield return new WaitUntil(() => !isUpgradePanelActive);
        }
    }

    private void ShowUpgradePanel()
    {
        isUpgradePanelActive = true;
        // Your code to enable/show the upgrade panel goes here
        // For example: upgradePanel.SetActive(true);
    }

    // Call this from your UI when the player closes the upgrade panel
    public void OnUpgradePanelClosed()
    {
        isUpgradePanelActive = false;
        // For example: upgradePanel.SetActive(false);
    }
}
