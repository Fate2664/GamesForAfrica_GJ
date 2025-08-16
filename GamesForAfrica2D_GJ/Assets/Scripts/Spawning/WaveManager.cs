using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
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
            spawning = true;
            int enemiesToSpawn = enemiesPerWave[currentWave];

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                enemySpawner.SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
}
