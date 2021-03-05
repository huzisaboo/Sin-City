using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    private bool m_isPressed = false;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        m_isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_isPressed = false;
    }

    public bool IsPressed()
    {
        return m_isPressed;
    }
   
}
