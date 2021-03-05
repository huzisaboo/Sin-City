using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    [SerializeField]
    private float m_stoppingDistance = 1;
    
    private SteeringAgent m_steeringAgent;
    private Animator m_animator;
    private Transform m_target;
    private bool m_isAttacking = false;
    // Start is called before the first frame update
    void Start()
    {
        m_steeringAgent = GetComponent<SteeringAgent>();
        m_animator = GetComponent<Animator>();
        m_target = m_steeringAgent.GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, m_target.position);
        Vector2 direction = (m_target.position - transform.position).normalized;
        float dot = Vector2.Dot(transform.right, direction);
        // Debug.Log(dot);
        //Debug.Log(distance);
        if((distance <= m_stoppingDistance) && !m_isAttacking)
        {
           // Debug.Log("Within range");
            if (dot <= -0.9f || dot >= 0.9f)
            {
                Attack(true);
            }
        }
       
        else if (distance > m_stoppingDistance && m_isAttacking)
        {
            Attack(false);
        }

        
    }



    private void Attack(bool value)
    {
        m_steeringAgent.enabled = !value;
        m_animator.SetBool("Attack", value);
        m_isAttacking = value;
    }
}
