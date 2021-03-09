using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Transform m_playerTransform;

    [SerializeField]
    private UnityEvent m_waveEndEvent;

    [SerializeField]
    private string m_winGameText;

    [SerializeField]
    private string m_lostGameText;

    private List<SteeringAgent> enemies = new List<SteeringAgent>();
    private int m_killCount = 0;
    private int m_score = 0;
    private bool m_gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
      //  EnemyWaves.Instance.StartWave();
    }

    // Update is called once per frame
    //void Update()
    //{

    //}

    public Transform GetPlayer()
    {
        return m_playerTransform;
    }

    public void AddEnemy(SteeringAgent p_enemy)
    {
        enemies.Add(p_enemy);
    }

    public void RemoveEnemy(SteeringAgent p_enemy)
    {
        Destroy(p_enemy.gameObject);
    }

    public void ClearDeadEnemies()
    {
        foreach(SteeringAgent enemy in enemies)
        {
            RemoveEnemy(enemy);
        }
        enemies.Clear();
    }

    public List<SteeringAgent> GetEnemyList()
    {
        return enemies;
    }

    public void EndWave()
    {
        
        m_killCount = 0;
        m_waveEndEvent.Invoke();
    }

    public void EndGame(bool p_win)
    {
        if(p_win)
        {
            UIManager.Instance.EnableGameOverPanel(true,m_winGameText);
        }
        else
        {
            UIManager.Instance.EnableGameOverPanel(true, m_lostGameText);
        }
        m_gameOver = true;
    }

    public int GetKillCount()
    {
        return m_killCount;
    }

    public void SetKillCount(int p_killCount)
    {
        m_killCount = p_killCount;
        m_score = p_killCount;
        UIManager.instance.SetScoreText(m_score);
    }

    public bool IsGameOver()
    {
        return m_gameOver;
    }

}
