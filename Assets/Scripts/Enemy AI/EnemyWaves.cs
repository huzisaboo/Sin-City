using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField]
    private List<int> m_Waves;  //Size of list determines no of waves. Each item in list determines no of enemies for that wave
    [SerializeField]
    private float m_enemySpawnDuration;
    private float m_enemyTimer;
    private float m_waveTimer;
    private EnemySpawner m_enemySpawner;
    private bool m_waveStarted = false;
    private float m_waveStartTime = 2.0f;
    private int m_waveEnemyCount;
    // Start is called before the first frame update
    void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        m_enemyTimer += Time.deltaTime;
        m_waveTimer += Time.deltaTime;

        if (m_waveTimer >= m_waveStartTime && !m_waveStarted && m_Waves.Count > 0)
        {
            StartWave();
        }
        if (m_waveStarted)
        {
            if (m_enemyTimer >= m_enemySpawnDuration && m_waveEnemyCount > 0)
            {
                m_enemySpawner.SpawnEnemy();
                m_enemyTimer = 0.0f;
                m_waveEnemyCount--;
                Debug.Log("Spawn");
            }

        }


    }

    public void StartWave()
    {
        m_waveStarted = true;
        m_waveEnemyCount = m_Waves[0];
    }

    public void EndWave()
    {
        m_Waves.RemoveAt(0);
        m_waveStarted = false;
        m_waveTimer = 0.0f;
    }
}
