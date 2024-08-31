using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeMove : MonoBehaviour
{
 private RectTransform m_RectTransform;
    public float speed;
    float maxDistance=350;
    public static bool IsActive=true;
    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if (!IsActive)return;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_RectTransform.anchoredPosition=new Vector2(m_RectTransform.anchoredPosition.x-0.1f*speed, m_RectTransform.anchoredPosition.y);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x + 0.1f * speed, m_RectTransform.anchoredPosition.y);

        }
        if (m_RectTransform.anchoredPosition.x > maxDistance)
        {
            m_RectTransform.anchoredPosition = new Vector2(maxDistance, m_RectTransform.anchoredPosition.y);
        }
        if(m_RectTransform.anchoredPosition.x < -maxDistance)
        {
            m_RectTransform.anchoredPosition = new Vector2(-maxDistance, m_RectTransform.anchoredPosition.y);
        }
    }
}
