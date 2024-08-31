using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PineconeMove : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private Vector2 initTransform;
    public float speed = 50;
    float maxDistance=350;
    public bool IsActive= false;
    Animator animator;
    BoxCollider2D boxCollider2D;

    private void OnEnable()
    {
        IsActive = false;
        speed = 50;
        maxDistance = 350;
        m_RectTransform.anchoredPosition = initTransform;
    }
    private void Start()
    {
        initTransform = m_RectTransform.anchoredPosition;
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
