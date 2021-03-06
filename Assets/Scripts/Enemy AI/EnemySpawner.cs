using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_enemyPrefabs;
    [SerializeField]
    private List<Transform> m_spawnPoints;
    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
        
    //}

    public void SpawnEnemy()
    {
        int randEnemyType = Random.Range(0, m_enemyPrefabs.Count);
     
        int randSpawnPoint = Random.Range(0, m_spawnPoints.Count);
        
        GameObject enemy = Instantiate(m_enemyPrefabs[randEnemyType], m_spawnPoints[randSpawnPoint].position, m_spawnPoints[randSpawnPoint].rotation);

    }
}
