using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Initial number of enemies to spawn in the first wave.")]
    public int initialEnemiesPerWave = 3;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 1f;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;

    private int currentWave = 0;

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
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}
