using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform spawnPoint;
    public GameObject[] enemyPrefab;
    public GameObject enemies_GameObject;


    public void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnPoint == null)
            return;

        int randomIndex = Random.Range(0, enemyPrefab.Length);
        GameObject spawnedEnemy = Instantiate(enemyPrefab[randomIndex], spawnPoint.position, Quaternion.identity);
        spawnedEnemy.transform.SetParent(enemies_GameObject.transform, this);
    }
}
