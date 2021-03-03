using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    private Vector3 m_input;
    private bool m_isTouched = false;
    private Vector2 m_joystickStartPoint;
    private Vector2 m_joystickEndPoint;

    private Vector2 m_offset;
    private Vector2 m_direction;

    [SerializeField]
    private Transform m_joystickBackground;
    [SerializeField]
    private Transform m_joystick;
    // Start is called before the first frame update
    void Start()
    {
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
            //Touch touch = Input.GetTouch(0);
            Vector3 mouseInput = Input.mousePosition;
            Vector2 touch = new Vector2(mouseInput.x, mouseInput.y);
            m_joystickEndPoint = touch;

            m_offset =  (m_joystickEndPoint - m_joystickStartPoint).normalized;
            m_direction = Vector2.ClampMagnitude(m_offset, 1.0f);

            if (RectTransformUtility.RectangleContainsScreenPoint(m_joystickBackground.GetComponent<RectTransform>(), touch))
            {

                m_joystick.position = m_joystickEndPoint;
            }
            else
            {
                m_joystick.position = ((touch - m_joystickStartPoint).normalized * (m_joystickBackground.GetComponent<RectTransform>().rect.width - 50f) + m_joystickStartPoint) * -0.1f;
            }

            return true;
        }

        return false;
    }

    public Vector2 GetInput()
    {
        return m_input;
    }
}
