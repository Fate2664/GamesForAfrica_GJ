using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Number of enemies to spawn in each wave. The length of this array determines the number of waves.")]
    public int[] enemiesPerWave; 
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 1f;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;

    private int currentWave = 0;
    private bool spawning = false;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < enemiesPerWave.Length)
        {
            Debug.Log($"Starting Wave {currentWave + 1}");

            int enemiesToSpawn = enemiesPerWave[currentWave];
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Debug.Log($"Spawning enemy {i + 1}/{enemiesToSpawn} in wave {currentWave + 1}");
                enemySpawner.SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            Debug.Log($"Wave {currentWave + 1} complete");

            currentWave++;
            if (currentWave < enemiesPerWave.Length)
            {
                Debug.Log($"Waiting {timeBetweenWaves} seconds before next wave...");
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        
        Debug.Log("All waves finished!");
    }

}
