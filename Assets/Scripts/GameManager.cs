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
    
    private List<SteeringAgent> enemies = new List<SteeringAgent>();


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
        if(enemies.Contains(p_enemy))
        {
            enemies.Remove(p_enemy);
        }
    }

    public List<SteeringAgent> GetEnemyList()
    {
        return enemies;
    }

    public void EndWave()
    {
        m_waveEndEvent.Invoke();
    }

    public void EndGame()
    {
        Debug.Log("Game Over");
    }
}
