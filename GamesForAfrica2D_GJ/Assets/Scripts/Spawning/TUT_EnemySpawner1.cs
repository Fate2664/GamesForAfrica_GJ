using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class TUT_EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform[] spawnPoint;
    public GameObject[] enemyPrefab;

    private List<Enemy> enemy;

    private void Start()
    {
       //enemy = enemyPrefab
    }
    public void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnPoint == null)
            return;

        int randomIndex = Random.Range(0, enemyPrefab.Length);
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            Instantiate(enemyPrefab[randomIndex], spawnPoint[i].position, Quaternion.identity);

        }   
    }
}
