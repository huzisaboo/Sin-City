using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 0.0f;

    [SerializeField]
    private VirtualJoystick m_virtualJoystick;

    [SerializeField]
    private VirtualButton m_attackButton;

    private Vector3 m_input = Vector3.zero;
    private Animator m_CharacterAnimator;
    private int m_attackAnimationHash = 0;
    private bool m_isAttacking = false;
  
    // Start is called before the first frame update
    private void Start()
    {
        if (GetComponent<Animator>())
        {
            m_CharacterAnimator = GetComponent<Animator>();
        }
        
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
        else if((!m_attackButton.IsPressed() && m_isAttacking))
        {
            m_isAttacking = false;
        }

        //------Move Logic-------
        m_input.x = m_virtualJoystick.GetInput().x;
        m_input.y = m_virtualJoystick.GetInput().y;


        //flip the game object if  main character is moving in the negative direction
        FlipObject();

        m_CharacterAnimator.SetFloat("Speed", m_input.sqrMagnitude);
        transform.position += m_input * m_speed * Time.deltaTime;
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
}