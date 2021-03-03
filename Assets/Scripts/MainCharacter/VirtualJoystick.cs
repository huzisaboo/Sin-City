using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    private Vector3 m_input;
    private Vector2 m_joystickStartPoint;
    private Vector2 m_joystickEndPoint;
    private Vector2 m_offset;
    private Vector2 m_direction;
    private bool m_isAttacked = false;
    
    [SerializeField]
    private float m_joystickOuterBoundOffset;
    [SerializeField]
    private Transform m_joystickBackground;
    [SerializeField]
    private Transform m_joystick;
    private RectTransform m_joystickBgRect;
    // Start is called before the first frame update
    void Start()
    {
        m_joystickBgRect = m_joystickBackground.GetComponent<RectTransform>();
        m_joystickStartPoint = m_joystick.position;
    }

    // Update is called once per frame
    void Update()
    {
       // bool isJoystick = joystickInput();

        if (JoystickInput())
        {
            m_input = new Vector3(m_direction.x, m_direction.y);
        }
        else
        {
            m_joystick.position = m_joystickStartPoint;
            m_input = Vector3.zero;
            m_input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

    bool JoystickInput()
    {
        if (Input.touchCount > 0 && !m_isAttacked)
        {
            Touch touch = Input.GetTouch(0);
            //Vector3 mouseInput = Input.mousePosition;
            //Vector2 touch = new Vector2(mouseInput.x, mouseInput.y);
            m_joystickEndPoint = touch.position;

            m_direction = (m_joystickEndPoint - m_joystickStartPoint).normalized;

            if(m_joystickBgRect != null)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(m_joystickBgRect, touch.position))
                {
                    //Within bounds
                    m_joystick.position = m_joystickEndPoint;
                }
                else
                {
                    //Outside bounds
                    m_joystick.position = (touch.position - m_joystickStartPoint).normalized * (m_joystickBgRect.rect.width - m_joystickOuterBoundOffset) + m_joystickStartPoint;
                }

                return true;

            }
        }

        return false;
    }

    public void Attack()
    {
        m_isAttacked = true;
    }

    public bool getAttckButton()
    {
        return m_isAttacked;
    }

    public void setAttackButton(bool attack)
    {
        m_isAttacked = attack;
    }

    public Vector2 GetInput()
    {
        return m_input;
    }
}
