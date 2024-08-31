using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeMove : MonoBehaviour
{
 private RectTransform m_RectTransform;
    public float speed;
    float maxDistance=350;
    public static bool IsActive=true;
    Animator animator;
    BoxCollider2D boxCollider2D;
    private void Start()
    {
        animator = GetComponent<Animator>();
        m_RectTransform = GetComponent<RectTransform>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
        if (!IsActive)return;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_RectTransform.anchoredPosition=new Vector2(m_RectTransform.anchoredPosition.x-0.1f*speed, m_RectTransform.anchoredPosition.y);
            animator.SetBool("IsRight", false);
            if (boxCollider2D.offset.x > 0)
            {
                boxCollider2D.offset = new Vector2(-boxCollider2D.offset.x, boxCollider2D.offset.y);
            }

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_RectTransform.anchoredPosition = new Vector2(m_RectTransform.anchoredPosition.x + 0.1f * speed, m_RectTransform.anchoredPosition.y);
            animator.SetBool("IsRight", true);
            if (boxCollider2D.offset.x < 0)
            {
                boxCollider2D.offset = new Vector2(-boxCollider2D.offset.x, boxCollider2D.offset.y);
            }

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
