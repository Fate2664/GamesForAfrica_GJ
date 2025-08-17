using System.Collections;
using UnityEngine;

public class TUT_WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [Tooltip("Initial number of enemies to spawn in the first wave.")]
    public int initialEnemiesPerWave = 3;

    [Header("Connections")]
    [SerializeField] private EnemySpawner enemySpawner;


    private void Start()
    {
        enemySpawner.SpawnEnemy();
    }

}
