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
    [SerializeField]
    private float m_joystickOuterBoundOffset;
    [SerializeField]
    private Transform m_joystickBackground;
    [SerializeField]
    private Transform m_joystick;
    private RectTransform m_joystickBgRect;
    private RectTransform m_joystickRect;
    // Start is called before the first frame update
    void Start()
    {
        m_joystickBgRect = m_joystickBackground.GetComponent<RectTransform>();
        m_joystickRect = m_joystick.GetComponent<RectTransform>();
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
        if (Input.GetMouseButton(0))
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

    public Vector2 GetInput()
    {
        return m_input;
    }
}
