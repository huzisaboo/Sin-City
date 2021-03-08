using System.Collections;
using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private Vector3 m_input = Vector3.zero;
    private Animator m_CharacterAnimator;
    private int m_attackAnimationHash = 0;
    private bool m_isAttacking = false;
    private float m_health = 0.0f;

    [SerializeField]
    private LayerMask m_layerMask;

    [SerializeField]
    private float m_maxHealth = 100.0f;

    [SerializeField]
    private float m_AttackDamage = 0.0f;

    [SerializeField]
    private float m_speed = 0.0f;

    [Header("--Joystick--")]
    [SerializeField]
    private VirtualJoystick m_virtualJoystick;

    [SerializeField]
    private VirtualButton m_attackButton;
    private bool m_hit = false;
    // Start is called before the first frame update
    private void Start()
    {
        if (GetComponent<Animator>())
        {
            m_CharacterAnimator = GetComponent<Animator>();
        }
        m_health = m_maxHealth;
       // m_layerMask = LayerMask.GetMask("obstacle");

        //hashing the animation from string to int for optimization
        m_attackAnimationHash = Animator.StringToHash("Base Layer.Attack");
    }

    // Update is called once per frame
    private void Update()
    {
        //-----Attack Logic------
        if ((Input.GetKeyDown(KeyCode.Space)) || (m_attackButton.IsPressed()) && m_isAttacking == false)
        {
            m_CharacterAnimator.SetTrigger("Attack");
            m_isAttacking = true;

            AttackEnemy();
            return;
        }
        //only check for animation attack state when the main character is attacking
        //if (m_isAttacking == true)
        //{
        //    if (m_CharacterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == m_attackAnimationHash)
        //    {
        //        return;
        //    }
        //    m_isAttacking = false;
        //   // m_virtualJoystick.setAttackButton(false);
        //}
        else if ((!m_attackButton.IsPressed() && m_isAttacking))
        {
            m_isAttacking = false;
        }

        //------Move Logic-------

        m_input.x = m_virtualJoystick.GetInput().x;
        m_input.y = m_virtualJoystick.GetInput().y;
        if (m_input == Vector3.zero)
        {
            m_input.x = Input.GetAxis("Horizontal");
            m_input.y = Input.GetAxis("Vertical");
        }

        //flip the game object if  main character is moving in the negative direction
        FlipObject();

        m_CharacterAnimator.SetFloat("Speed", m_input.sqrMagnitude);
        transform.position += m_input * m_speed * Time.deltaTime;
    }

    private void AttackEnemy()
    {
        //RaycastHit2D[] hitArray = Physics2D.BoxCastAll(transform.position, transform.localScale, 0, transform.right ,2.0f, m_layerMask);
        // RaycastHit2D[] hitArray = Physics2D.CircleCastAll(transform.position, 1.0f, transform.right, 1.0f,m_layerMask);

        //foreach(RaycastHit2D hit in hitArray){
        //     Debug.Log(hit.transform.name);
        //     if(hit.transform.CompareTag("enemy"))
        //     {
        //         hit.transform.GetComponent<EnemyActions>().TakeDamage(m_AttackDamage);
        //     }
        // }

        RaycastHit2D[] hitArray = Physics2D.CircleCastAll(transform.position, 0.2f, transform.right, 1.0f, m_layerMask);
       foreach (RaycastHit2D hit in hitArray)
        {
            if (hit.transform.CompareTag("enemy"))
            {
                EnemyActions enemy = hit.transform.GetComponent<EnemyActions>();
                if(!enemy.IsDead())
                {
                        enemy.TakeDamage(m_AttackDamage);
                }
            }
        }

    }

    private void FlipObject()
    {
        if (m_input.x < 0.0f)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
        else if (m_input.x > 0.0f)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        m_health -= damage;
        m_CharacterAnimator.SetTrigger("isHit");
        if (m_health <= 0.0f)
        {
            m_CharacterAnimator.SetTrigger("die");
            GetComponent<MainCharacterController>().enabled = false;
            StartCoroutine(waitForSeconds(3.0f));
        }
    }

    private IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);


    }
}