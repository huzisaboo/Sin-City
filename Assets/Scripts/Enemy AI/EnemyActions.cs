using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public bool m_kill = false;

    private SteeringAgent m_steeringAgent;
    private Animator m_animator;
    private Transform m_target;
    private SpriteRenderer m_spriteRenderer;
    private MainCharacterController m_mainCharacterController;
    private Vector2 m_direction;
    private bool m_isAttacking = false;
    private float m_attackCooldownTimer = 0.0f;
    private float m_attackTimer = 0.0f;
    private float m_distance;
    private float m_directionDotProduct;
    private float m_applyDamageTime = 0.0f;
    private float m_health = 0.0f;

    [Header("--Health--")]
    [SerializeField]
    private float m_maxHealth = 100.0f;

    [Header("--Attack--")]
    [SerializeField]
    private float m_AttackDamage = 10.0f;

    [SerializeField]
    private float m_attackDuration = 1.0f;

    [SerializeField]
    private float m_attackCooldown = 3.0f;

    [SerializeField]
    private float m_stoppingDistance = 0.5f;

    [SerializeField]
    private float m_levelThreshold = 0.95f;

    // Start is called before the first frame update
    private void Start()
    {
        m_steeringAgent = GetComponent<SteeringAgent>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_target = m_steeringAgent.GetTarget();
        m_applyDamageTime = m_attackDuration * 0.5f;
        m_mainCharacterController = m_target.GetComponent<MainCharacterController>();
        m_health = m_maxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_isAttacking == true)
        {
            AttackTimeLogic();
            return;
        }
        AttackPlayer();
    }

    private void AttackTimeLogic()
    {
        m_attackTimer += Time.deltaTime;
        if (m_attackTimer == (m_applyDamageTime * 0.5f))
        {
            Debug.Log("Damage");
        }
        if (m_attackTimer > m_attackDuration)
        {
            SetAttackAnim(false);
            m_isAttacking = false;
            m_attackCooldownTimer = 0.0f;
            m_attackTimer = 0.0f;
        }
    }

    private void AttackPlayer()
    {
        m_attackCooldownTimer += Time.deltaTime;
        if (m_attackCooldownTimer > m_attackCooldown)
        {
            m_distance = Vector2.Distance(transform.position, m_target.position);
            m_direction = (m_target.position - transform.position).normalized;
            m_directionDotProduct = Vector2.Dot(transform.right, m_direction);
            if ((m_distance <= m_stoppingDistance) && !m_isAttacking)
            {
                if (m_directionDotProduct <= -m_levelThreshold)
                {
                    //if in Front of Player , turn towards the player and attack
                    if (!m_spriteRenderer.flipX)
                    {
                        m_spriteRenderer.flipX = true;
                    }
                    m_mainCharacterController.TakeDamage(m_AttackDamage);
                    SetAttackAnim(true);
                }
                else if (m_directionDotProduct >= m_levelThreshold)
                {
                    //if Behind the Player , turn towards the player and attack
                    if (m_spriteRenderer.flipX)
                    {
                        m_spriteRenderer.flipX = false;
                    }
                    m_mainCharacterController.TakeDamage(m_AttackDamage);
                    SetAttackAnim(true);
                }
            }
            else if (m_distance > m_stoppingDistance && m_isAttacking)
            {
                SetAttackAnim(false);
            }
        }
    }

    private void SetAttackAnim(bool value)
    {
        m_steeringAgent.enabled = !value;
        m_animator.SetBool("Attack", value);
        m_isAttacking = value;
    }

    private void Die()
    {
        GameManager.Instance.RemoveEnemy(m_steeringAgent);
        if (GameManager.Instance.GetEnemyList().Count == 0)
        {
            GameManager.Instance.EndWave();
        }
        Destroy(this.gameObject);
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        Debug.Log(m_health);
        if (m_health <= 0.0f)
        {
            Die();
        }
    }
}