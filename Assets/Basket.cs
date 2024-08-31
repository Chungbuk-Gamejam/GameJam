using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Basket : MonoBehaviour
{
    private int successNeededCount = 5;
    private int count = 0;

    public float rayLength = 10f; // Ray�� ���� ����
    private RectTransform m_rectTransform;
    BoxCollider2D m_boxCollider;
    ContactFilter2D contactFilter = new ContactFilter2D()
    {
        useLayerMask = true,

    };
    List<Collider2D> overlappingColliders = new List<Collider2D>();

    private void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
        contactFilter.SetLayerMask(Physics2D.AllLayers);
        m_boxCollider = GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {
        
        
        int tmp= m_boxCollider.OverlapCollider(contactFilter, overlappingColliders);
        if (tmp>=1)
        {
            if (overlappingColliders[0].gameObject.tag == "cone")
            {
                //���� ���� �߰�
                count++;
            }
            else if (overlappingColliders[0].gameObject.tag == "bug")
            {

                GenerateBugAndCone.IsActive = false;
                PineconeMove.IsActive = false;
                //���� ���� �߰�
            }
            else
            {
                Debug.Log("collision error");
            }
            Destroy(overlappingColliders[0].gameObject);
            if (successNeededCount <= count)
            {
                GenerateBugAndCone.IsActive = false;
                PineconeMove.IsActive = false;

                //����! ���� �߰�
            }
        }
    }
    
}
