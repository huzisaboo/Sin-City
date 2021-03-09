using System.Collections.Generic;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField]
    private List<int> m_Waves;  //Size of list determines no of waves. Each item in list determines no of enemies for that wave

    [SerializeField]
    private float m_waveStartTime = 2.0f;   //Defines the cooldown time before a new wave begins

    [SerializeField]
    private float m_enemySpawnDuration;

    [Header("Spawn Boss Enemy every # wave")]
    [SerializeField]
    private int m_spawnBossWaveNumber;

    private float m_enemyTimer;
    private float m_waveTimer;
    private EnemySpawner m_enemySpawner;
    private bool m_waveStarted = false;
    private bool m_isBossWave = false;
    private int m_waveCounter = 0;
    private int m_waveEnemyCount = 0;
    private int m_numberOfBossEnemyToSpawn = 0;

    private
    // Start is called before the first frame update
    void Start()
    {
        m_enemySpawner = GetComponent<EnemySpawner>();
    }

    // Update is called once per frame
    private void Update()
    {
        m_enemyTimer += Time.deltaTime;
        m_waveTimer += Time.deltaTime;

        if (m_waveTimer >= m_waveStartTime && !m_waveStarted && m_Waves.Count > 0)
        {
            StartWave();
        }
        if (m_waveStarted)
        {
            if (m_enemyTimer >= m_enemySpawnDuration )
            {
                if(m_isBossWave == true & m_numberOfBossEnemyToSpawn > 0)
                {
                    m_enemySpawner.SpawnBossEnemy(); //resulting quotient will give the number of boss enemies to spawn
                    m_numberOfBossEnemyToSpawn--;
                }
                if(m_isBossWave == false && m_waveEnemyCount > 0)
                {
                    m_enemySpawner.SpawnEnemy();
                    m_enemyTimer = 0.0f;
                    m_waveEnemyCount--;
                }
            }
        }
    }

    public void StartWave()
    {
        GameManager.Instance.ClearDeadEnemies();
        //Reset Health Every wave
        GameManager.Instance.GetPlayer().GetComponent<MainCharacterController>().ResetHealth(100);
        m_waveStarted = true;
        m_waveEnemyCount = m_Waves[0];
        m_waveCounter++;
        //Spawn Boss Enemy if it the # wave
        if (m_waveCounter % m_spawnBossWaveNumber == 0)
        {
            m_numberOfBossEnemyToSpawn = m_waveCounter / m_spawnBossWaveNumber;
            m_isBossWave = true;
        }
        if (m_Waves.Count == 1)
        {
            m_waveCounter = 0;
        }
        UIManager.Instance.SetWaveCountText(m_waveCounter);
    }

    public void EndWave()
    {
        m_Waves.RemoveAt(0);
        m_waveStarted = false;
        m_waveTimer = 0.0f;
        if (m_Waves.Count == 0)
        {
            GameManager.Instance.EndGame(true);
        }
        if(m_isBossWave == true)
        {
            m_isBossWave = false;
        }
    }
}