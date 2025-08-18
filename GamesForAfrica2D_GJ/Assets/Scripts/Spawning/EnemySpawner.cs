using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform[] spawnPoint;
    public GameObject[] enemyPrefab;


    public void SpawnEnemy()
    {
        if (enemyPrefab.Length == 0 || spawnPoint == null)
            return;

        for (int i = 0; i < spawnPoint.Length; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            GameObject newEnemy = Instantiate(
                enemyPrefab[randomIndex],
                spawnPoint[i].position,
                Quaternion.identity);
            newEnemy.transform.localScale = enemyPrefab[randomIndex].transform.localScale;
        }

    }
}
