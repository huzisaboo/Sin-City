using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("--Enemy--")]

    [SerializeField]
    private List<GameObject> m_enemyPrefabs;
    
    [SerializeField]
    private List<Transform> m_spawnPoints;

    [Header("--Boss Enemy--")]
    [SerializeField]
    private Transform m_bossEnemyPrefab;

    [SerializeField]
    private Transform m_bossSpawnPoint;





    public void SpawnEnemy()
    {
        int randEnemyType = Random.Range(0, m_enemyPrefabs.Count);

        // int randSpawnPoint = Random.Range(0, m_spawnPoints.Count);

        //spawn enemies in Each spawn point simultaneously
        for (int i = 0; i < m_spawnPoints.Count; i++)
        {
            Instantiate(m_enemyPrefabs[randEnemyType], m_spawnPoints[i].position, m_spawnPoints[i].rotation);
        }

        //GameObject enemy = Instantiate(m_enemyPrefabs[randEnemyType], m_spawnPoints[randSpawnPoint].position, m_spawnPoints[randSpawnPoint].rotation);

    }

    public void SpawnBossEnemy()
    {
           Instantiate(m_bossEnemyPrefab.gameObject, m_bossSpawnPoint.position * Random.Range(10,20), m_bossSpawnPoint.rotation);
    }
}
