using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform spawnPoint;
    public GameObject[] enemyPrefab;


    public void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnPoint == null)
            return;

        int randomIndex = Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomIndex], spawnPoint.position, Quaternion.identity);
    }
}
