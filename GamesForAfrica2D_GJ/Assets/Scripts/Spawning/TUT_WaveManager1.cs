using System.Collections;
using UnityEngine;

public class TUT_WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Initial number of enemies to spawn in the first wave.")]
    public int initialEnemiesPerWave = 3;
    public float timeBetweenWaves = 5f;
    public float timeBetweenSpawns = 1f;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;


    private void Start()
    {
        enemySpawner.SpawnEnemy();
    }

}
