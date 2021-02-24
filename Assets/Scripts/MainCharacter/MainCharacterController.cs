using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 0.0f;
    private Vector3 m_input = Vector3.zero;
    private Animator m_CharacterAnimator;



    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Animator>())
        {
            m_CharacterAnimator = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Attack Logic
        if(Input.GetKey(KeyCode.Space))
        {
            m_CharacterAnimator.SetTrigger("Attack");
            return;
        }
        //Move Logic
        m_input.x = Input.GetAxis("Horizontal");
        m_input.y = Input.GetAxis("Vertical");
        if(m_input.x < 0.0f)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);

        }
        m_CharacterAnimator.SetFloat("Speed", m_input.sqrMagnitude);


        transform.position += m_input * m_speed * Time.deltaTime;
    }
}
