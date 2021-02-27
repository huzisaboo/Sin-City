using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 0.0f;

    private Vector3 m_input = Vector3.zero;
    private Animator m_CharacterAnimator;
    private int m_attackAnimationHash = 0;
    private bool m_isAttacking = false;
    private SpriteRenderer m_spriteRenderer;

    // Start is called before the first frame update
    private void Start()
    {
        if (GetComponent<Animator>())
        {
            m_CharacterAnimator = GetComponent<Animator>();
        }
        if(GetComponent<SpriteRenderer>())
        {
            m_spriteRenderer = GetComponent<SpriteRenderer>();
        }
        //hashing the animation from string to int for optimization
        m_attackAnimationHash = Animator.StringToHash("Base Layer.Attack");
    }

    // Update is called once per frame
    private void Update()
    {
        //-----Attack Logic------
        if (Input.GetKeyDown(KeyCode.Space) && m_isAttacking == false)
        {
            m_CharacterAnimator.SetTrigger("Attack");
            m_isAttacking = true;
            return;
        }
        //only check for animation attack state when the main character is attacking
        if(m_isAttacking == true)
        {
            if (this.m_CharacterAnimator.GetCurrentAnimatorStateInfo(0).fullPathHash == m_attackAnimationHash)
            {
                return;
            }
            m_isAttacking = false;
        }


        //------Move Logic-------
        m_input.x = Input.GetAxis("Horizontal");
        m_input.y = Input.GetAxis("Vertical");
        //flip the game object if  main character is moving in the negative direction
        if (m_input.x < 0.0f)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            //m_spriteRenderer.flipX = true;
        }
        else if(m_input.x > 0.0f)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            //m_spriteRenderer.flipX = false;

        }
        
        m_CharacterAnimator.SetFloat("Speed", m_input.sqrMagnitude);
        transform.position += m_input * m_speed * Time.deltaTime;
    }
}