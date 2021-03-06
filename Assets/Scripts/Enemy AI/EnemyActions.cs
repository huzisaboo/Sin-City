using UnityEngine;
using UnityEngine.Events;

public class EnemyActions : MonoBehaviour
{
    [SerializeField]
    private float m_stoppingDistance = 1;

    [SerializeField]
    private float m_levelThreshold = 0.95f;

    private SteeringAgent m_steeringAgent;
    private Animator m_animator;
    private Transform m_target;
    private bool m_isAttacking = false;
    private SpriteRenderer m_spriteRenderer;
    public bool m_kill = false;
    // Start is called before the first frame update
    void Start()
    {
        m_steeringAgent = GetComponent<SteeringAgent>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_target = m_steeringAgent.GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, m_target.position);
        Vector2 direction = (m_target.position - transform.position).normalized;
        float dot = Vector2.Dot(transform.right, direction);
        //Debug.Log(dot);
       // Debug.Log(distance);
        if ((distance <= m_stoppingDistance) && !m_isAttacking)
        {
            // Debug.Log("Within range");
            if (dot <= -m_levelThreshold)
            {
                //In Front of Player
                if (!m_spriteRenderer.flipX)
                {
                    m_spriteRenderer.flipX = true;
                }
                Attack(true);
            }
            else if(dot >= m_levelThreshold)
            {
                //Behind Player
                if (m_spriteRenderer.flipX)
                {
                    m_spriteRenderer.flipX = false;
                }
                Attack(true);
            }
            
        }

        else if (distance > m_stoppingDistance && m_isAttacking)
        {
            Attack(false);
        }

        if(m_kill)
        {
            Die();
        }

    }



    private void Attack(bool value)
    {
        m_steeringAgent.enabled = !value;
        m_animator.SetBool("Attack", value);
        m_isAttacking = value;
    }

    private void Die()
    {
        GameManager.Instance.RemoveEnemy(m_steeringAgent);
        if(GameManager.Instance.GetEnemyList().Count==0)
        {
            GameManager.Instance.EndWave();
        }
        Destroy(this.gameObject);
    }
}




    //private SteeringAgent m_steeringAgent;
    //private Animator m_animator;
    //private Transform m_target;
    //private Vector2 m_direction;
    //private bool m_isAttacking = false;
    //private float m_distance;
    //private float m_directionDotProduct;
    //private float m_attackCooldownTimer = 0.0f;
    //private float m_attackTimer = 0.0f;
    //[SerializeField]
    //private float m_attackDuration = 0.0f;

    //[SerializeField]
    //private float m_attackCooldown = 0.0f;

    //[SerializeField]
    //private float m_stoppingDistance = 1;

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    m_steeringAgent = GetComponent<SteeringAgent>();
    //    m_animator = GetComponent<Animator>();
    //    m_target = m_steeringAgent.GetTarget();
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //    if (m_isAttacking == true)
    //    {
    //        m_attackTimer += Time.deltaTime;
    //        if (m_attackTimer > m_attackDuration)
    //        {
    //            Attack(false);
    //            m_isAttacking = false;
    //            m_attackCooldownTimer = 0.0f;
    //            m_attackTimer = 0.0f;
    //        }
    //        return;
    //    }
    //    AttackPlayer();

    //}

    //private void AttackPlayer()
    //{
    //    m_attackCooldownTimer += Time.deltaTime;
    //    if (m_attackCooldownTimer > m_attackCooldown)
    //    {
    //        m_distance = Vector2.Distance(transform.position, m_target.position);
    //        m_direction = (m_target.position - transform.position).normalized;
    //        m_directionDotProduct = Vector2.Dot(transform.right, m_direction);
    //        if ((m_distance <= m_stoppingDistance) && !m_isAttacking)
    //        {
    //            if (m_directionDotProduct <= -0.9f || m_directionDotProduct >= 0.9f)
    //            {
    //                //warp behind the target and attack
    //                //transform.position = new Vector3(m_target.position.x - 1.0f, m_target.position.y);
    //                //transform.rotation = new Quaternion(-m_target.rotation.x, -m_target.rotation.y, 0, -m_target.rotation.w);
    //                //Debug.Log(transform.rotation);
    //                Attack(true);
    //            }
    //        }
    //        //else if (m_distance > m_stoppingDistance && m_isAttacking)
    //        //{
    //        //    Attack(false);
    //        //}

    //    }
    //}

    //private void Attack(bool value)
    //{
    //    m_steeringAgent.enabled = !value;
    //    m_animator.SetBool("Attack", value);
    //    m_isAttacking = value;
    //}
//}