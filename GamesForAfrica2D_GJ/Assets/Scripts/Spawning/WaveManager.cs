using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Initial number of enemies to spawn in the first wave.")]
    public int initialEnemiesPerWave = 3;
    public float timeBetweenSpawns = 1f;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject upgradePanel;
    private int currentWave = 0;
    private UpgradeManager upgradeManager;
    private bool isUpgradePanelActive = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
        upgradePanel.SetActive(false); 
        upgradeManager = upgradePanel.GetComponent<UpgradeManager>();
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

            // Wait until all enemies are destroyed before showing the upgrade panel
            yield return new WaitUntil(() => FindObjectsOfType<Enemy>().Length == 0);

            // Show upgrade panel and wait until it is closed
            ShowUpgradePanel();
            yield return new WaitUntil(() => !isUpgradePanelActive);
        }
    }

    private void ShowUpgradePanel()
    {
        isUpgradePanelActive = true;
        upgradePanel.SetActive(true);
    }

    // Call this from your UI when the player closes the upgrade panel
    public void OnUpgradePanelClosed()
    {
        isUpgradePanelActive = false;
        upgradePanel.SetActive(false);
    }
}
