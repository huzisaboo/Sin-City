using UnityEngine;

public class ContextBasedSteeringBehaviour : SteeringBehaviourBase
{
    public Transform targetObj;

    private Vector3[] m_intrest;
    private Vector3[] m_danger;
    private Vector3[] m_direction;
    private Vector3 m_result;
    private bool m_isHit = false;
    private bool m_isDangerVectorZero = false;
    [SerializeField]
    private int m_numberOfRays = 0;

    [SerializeField]
    private Transform m_parent;

    [SerializeField]
    private float m_maxForce;

    // Start is called before the first frame update
    private void Start()
    {
        m_intrest = new Vector3[m_numberOfRays];
        m_danger = new Vector3[m_numberOfRays];
        m_direction = new Vector3[m_numberOfRays];
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
            m_direction[i] = new Vector3(Mathf.Cos(angle) * 5, Mathf.Sin(angle) * 5 , 0 ).normalized;
        }
    }

    public override Vector2 calculateForce()
    {
        calcuateDangerDirection();
        calcuateIntrestDirection();
        Vector3 res = calculateFinaDirection();
        Vector2 finalVector = new Vector2(res.x, res.y).normalized;
        // 10.0 is the runrning power (maxforce)
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
        Vector3 moveTowrdsDirectio = Vector3.Normalize(targetObj.position - m_parent.position);
        float dot = 0.0f;
        for (int i = 0; i < m_numberOfRays; i++)
        {
            dot = Vector3.Dot(moveTowrdsDirectio, m_direction[i]);
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
        Vector3 otherPosiiton = Vector3.zero;
        Vector3 moveTowrdsDirectio = Vector3.zero;
        m_isHit = false;
        //raycast loop
        for (int i = 0; i < m_numberOfRays; i++)
        {
            hit = Physics2D.Raycast(m_parent.position, m_direction[i], 4);
            if (hit)
            {

                if (hit.transform.CompareTag("sphere"))
                {
                    otherPosiiton = hit.transform.position;
                    moveTowrdsDirectio = Vector3.Normalize(otherPosiiton - m_parent.position);

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
                    m_isDangerVectorZero = false;
                    m_isHit = true;
                }

                //after the first ray hit , stop the loop
                break;
            }
        }

        // if nothing is hit make the danger array vales as 0 vector
        //only set the danger vector array to zero only if it is non zero
        if (m_isDangerVectorZero == false)
        {
            if (m_isHit == false)
            {
                m_isDangerVectorZero = true;
                for (int i = 0; i < m_numberOfRays; i++)
                {
                    m_danger[i] = Vector3.zero;
                }
            }
        }

    }
}