using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private Text m_waveCountText;

    [SerializeField]
    private float m_waveUIDuration = 2.0f;

    [SerializeField]
    private GameObject m_gameOverPanel;
    [SerializeField]
    private Text m_winLoseText;

    [SerializeField]
    private Slider m_healthBarUI;

    [SerializeField]
    private Text m_score;

    private float m_waveUITimer;


    private void Start()
    {
        if(m_waveCountText.enabled)
        {
            m_waveCountText.enabled = false;
        }

        SetScoreText(0);
    }
    private void Update()
    {
        if(m_waveCountText.enabled)
        {
            m_waveUITimer += Time.deltaTime;

            if(m_waveUITimer >= m_waveUIDuration)
            {
                m_waveCountText.enabled = false;
                m_waveUITimer = 0.0f;
            }
        }
        
    }

    public void SetScoreText(int score)
    {
        m_score.text = "Score: " + score;
    }


    public Slider GetHealthBarUI()
    {
        return m_healthBarUI;
    }

    public void SetWaveCountText(int p_waveCount)
    {
        if(p_waveCount == 0)
        {
            m_waveCountText.text = "Final Wave";
        }
        else
        {
            m_waveCountText.text = "Wave " + p_waveCount.ToString();
        }
        m_waveCountText.enabled = true;
    }

    public void EnableGameOverPanel(bool p_value,string p_winloseText)
    {
        m_winLoseText.text = p_winloseText;
        StartCoroutine(waitForSeconds(2.0f));
    }

    private IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        m_gameOverPanel.SetActive(true);
    }
}
