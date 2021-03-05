using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Vector2 m_input = Vector2.zero;

    private Vector2 m_joystickStartPoint;
    private Vector2 m_joystickEndPoint;
    private Vector2 m_direction;

    [SerializeField]
    private float m_joystickOuterBoundOffset;
    
    [SerializeField]
    private Transform m_joystick;
    private RectTransform m_joystickBgRect;
    // Start is called before the first frame update
    void Start()
    {
        m_joystickBgRect = GetComponent<RectTransform>();
        m_joystick = transform.GetChild(0);
        m_joystickStartPoint = m_joystick.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_joystickEndPoint = eventData.position;
        m_direction = (m_joystickEndPoint - m_joystickStartPoint).normalized;
        if (m_joystickBgRect != null)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(m_joystickBgRect, eventData.position))
            {
                //Within bounds
                m_joystick.position = m_joystickEndPoint;
            }
            else
            {
                //Outside bounds
                m_joystick.position = (eventData.position - m_joystickStartPoint).normalized * (m_joystickBgRect.rect.width - m_joystickOuterBoundOffset) + m_joystickStartPoint;
            }

            m_input = m_direction;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_joystick.position = m_joystickStartPoint;
        m_input = Vector2.zero;
    }


    public Vector2 GetInput()
    {
        return m_input;
    }

}
