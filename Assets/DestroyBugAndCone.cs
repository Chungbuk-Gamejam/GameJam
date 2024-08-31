using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBugAndCone : MonoBehaviour
{
    private RectTransform m_RectTransform;
    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (m_RectTransform.anchoredPosition.y < -250) Destroy(gameObject);
    }
}
