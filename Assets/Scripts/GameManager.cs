using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Transform m_playerTransform;
    

    // Start is called before the first frame update
    void Start()
    {
      //  EnemyWaves.Instance.StartWave();
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    public Transform GetPlayer()
    {
        return m_playerTransform;
    }
}
