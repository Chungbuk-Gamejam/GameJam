using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    RectTransform m_rectTransform;
    public float speed;
    public GameObject touchTxt;
    // Start is called before the first frame update
    void Start()
    {
        m_rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_rectTransform.anchoredPosition.y < 770f)
        {
            int weight = 1;
            if (Input.GetMouseButton(0)) weight = 3;
            m_rectTransform.anchoredPosition = new Vector2(m_rectTransform.anchoredPosition.x, m_rectTransform.anchoredPosition.y + speed * Time.deltaTime*weight);
        }
        else if (Input.GetMouseButtonDown(0))
        {
            touchTxt.SetActive(true);

            StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"Meoyoung"));
        }
        else
        {
            touchTxt.SetActive(true);
        }
    }
}
