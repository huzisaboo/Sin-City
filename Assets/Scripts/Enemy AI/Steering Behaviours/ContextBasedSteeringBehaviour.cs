using UnityEngine;

public class ContextBasedSteeringBehaviour : SteeringBehaviourBase
{

    private Vector3[] m_intrest;
    private Vector3[] m_danger;
    private Vector3[] m_direction;
    private Vector3 m_result;
    private bool m_isHit = false;
    private bool m_isDangerArrayZero = false;
    private bool m_isPlayerHit = false;
    private LayerMask m_obstacleLayerMask;
    private SteeringAgent m_steeringAgent;
    private Vector3 m_intrestDirectionVector;
    private Vector3 m_dangerDirectionVector;
    private Transform targetObj;


    [SerializeField]
    private int m_numberOfRays = 0;
    [SerializeField]
    private float m_detectionRange = 0.0f;
    [SerializeField]
    private Transform m_parent;
    [SerializeField]
    private float m_maxForce = 0.0f;

    private void Awake()
    {
        targetObj = GameManager.Instance.GetPlayer();
        m_steeringAgent = GetComponentInParent<SteeringAgent>();
        if (m_steeringAgent != null && m_steeringAgent.GetTarget()==null)
        {
            m_steeringAgent.SetTarget(targetObj);
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        m_intrest = new Vector3[m_numberOfRays];
        m_danger = new Vector3[m_numberOfRays];
        m_direction = new Vector3[m_numberOfRays];
        m_obstacleLayerMask = LayerMask.GetMask("obstacle");

        
        float angle = 0;

        for (int i = 0; i < m_numberOfRays; i++)
        {
            m_danger[i] = Vector3.zero;
        }

        //create vectors in a circle
        for (int i = 0; i < m_numberOfRays; i++)
        {
            angle = i * 2 * Mathf.PI / m_numberOfRays;
            //only cast in front of the object
            //angle *= 0.5f;
            m_direction[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0).normalized;
        }
    }

    public override Vector2 calculateForce()
    {
        calcuateDangerDirection();
        calcuateIntrestDirection();
        Vector3 res = calculateFinaDirection();
        Vector2 finalVector = new Vector2(res.x, res.y).normalized;
        return finalVector * m_maxForce;
    }

    private Vector3 calculateFinaDirection()
    {
        m_result = Vector3.zero;

        //calcuate the final resultant intrest array (can be modified to get different behavious)
        for (int i = 0; i < m_numberOfRays; i++)
        {
            Debug.DrawRay(m_parent.position, m_danger[i] * 2.0f, Color.red);
            //eleminate all the intrest array index corresponding to the danger array index
            if (m_danger[i] != Vector3.zero)
            {
                m_intrest[i] = Vector3.zero;
            }
        }

        //get the highest magnituted vector
        for (int i = 0; i < m_numberOfRays; i++)
        {
            if (m_result.magnitude < m_intrest[i].magnitude)
            {
                m_result = m_intrest[i];
            }
            Debug.DrawRay(m_parent.position, m_intrest[i] * 3.0f, Color.green);
        }
        Vector3.Normalize(m_result);
        Debug.DrawRay(m_parent.position, m_result * 4.0f, Color.cyan);
        return m_result;
    }

    //Move to target logic
    private void calcuateIntrestDirection()
    {
        //vector between target and current object
        m_intrestDirectionVector = Vector3.Normalize(targetObj.position - m_parent.position);
        // turn the Vector by -90 degrees
        if (m_isPlayerHit == true)
        {
            m_intrestDirectionVector = Quaternion.AngleAxis(-90, Vector3.forward) * m_intrestDirectionVector;
        }
        float dot = 0.0f;
        for (int i = 0; i < m_numberOfRays; i++)
        {
            dot = Vector3.Dot(m_intrestDirectionVector, m_direction[i]);
            if (dot > 0)
            {
                m_intrest[i] = (m_direction[i] * dot);
            }
            else
            {
                // less desirable directions , basically in the opposite direction of the target
                m_intrest[i] = (m_direction[i] * -dot * 0.3f);
            }
        }
    }

    //avoid obsticles logic
    private void calcuateDangerDirection()
    {
        RaycastHit2D hit;
        //Vector3 otherPosiiton = Vector3.zero;
        Vector3 m_dangerDirectionVector = Vector3.zero;
        m_isHit = false;
        //raycast loop
        for (int i = 0; i < m_numberOfRays; i++)
        {
            hit = Physics2D.Raycast(m_parent.position, m_direction[i], m_detectionRange, m_obstacleLayerMask);
            if (hit)
            {
                string hitObjectTag = hit.transform.tag;
                switch (hitObjectTag)
                {
                    case "obstacle":
                        {
                            //vector between hit object and current object
                            m_dangerDirectionVector = Vector3.Normalize(hit.transform.position - m_parent.position);
                            MoveAwayFromObstacle(m_dangerDirectionVector);
                            break;
                        }
                    //case "enemy":
                    //    {
                    //        moveTowrdsDirectio = Vector3.Normalize(hit.transform.position - m_parent.position);
                    //        m_result = -moveTowrdsDirectio;
                    //        break;
                    //    }
                    case "player":
                        {
                            m_isPlayerHit = true;
                            break;
                        }
                }
                //after the first ray is hit break out of the for loop
                break;
            }
            else if(m_isPlayerHit == true)
            {
                m_isPlayerHit = false;
            }
        }

        // if nothing is hit make the danger array vales as 0 vector
        MakeDangerArrayZero();
    }

    private void MakeDangerArrayZero()
    {
        // only set the array to zero if it is non zero
        if (m_isDangerArrayZero == false)
        {
            if (m_isHit == false)
            {
                m_isDangerArrayZero = true;
                // if nothing is hit make the danger array vales as 0 vector
                for (int i = 0; i < m_numberOfRays; i++)
                {
                    m_danger[i] = Vector3.zero;
                }
            }
        }
    }

    private void MoveAwayFromObstacle(Vector3 moveTowrdsDirectio)
    {
        float dot = 0.0f;
        for (int j = 0; j < m_numberOfRays; j++)
        {
            dot = Vector3.Dot(moveTowrdsDirectio, m_direction[j]);
            if (dot > 0)
            {
                m_danger[j] = (m_direction[j] * dot);
            }
            else
            {
                m_danger[j] = Vector3.zero;
            }
        }
        m_isDangerArrayZero = false;
        m_isHit = true;
    }
}