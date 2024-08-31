using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IntroScript2 : MonoBehaviour
{
    private string sentence="ûȭ\n\n130�� �ֱ�� �Ͼ�� �ź�����\n\nûȭ�� ���� �� ����� ���� �� ���� ���� �ִ� ���� �ϳ��� �̷����ٴ� �ҹ��� �ִ�.\n\n���� ȣ��ɿ� �̲��� ûȭ ������ ���� ���� �� ���� ķ���� �Դ�.v�� �� ��vk�� �װ� �׷��� ������ �� �� ����..?\n\n�׳� <size= 50> ��ġ���� </size> �� �ظԾ�߰ڴ�! ������";
    private TextMeshProUGUI m_TextMeshProUGUI;
    public GameObject Character;
    public GameObject SpeechBubble;
    void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {

        bool localFlag = false;
        string tmpString = "";

        foreach (char letter in sentence.ToCharArray())
        {
            if (letter=='v')
            {
                yield return new WaitForSeconds(1.5f);
                m_TextMeshProUGUI.text ="";

                continue;
            }
            if(letter == 'k')
            {
                GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x - 270, GetComponent<RectTransform>().anchoredPosition.y);
                m_TextMeshProUGUI.color = new Color(0, 0, 0, 255);
                Character.SetActive(true);
                SpeechBubble.SetActive(true);
                continue;
            }

            if (letter == '<' || localFlag)
            {
                localFlag = true;
                tmpString += letter;
                if (letter == '>')
                {
                    
                        m_TextMeshProUGUI.text += tmpString;
                       
                    
                    tmpString = "";
                    localFlag = false;
                }
                continue;
            }

           
            yield return null;
            m_TextMeshProUGUI.text += letter;
            yield return null;
            yield return new WaitForSeconds(0.07f); //��¼ӵ� ������ ������ �� �ֵ��� / ��� ���


        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"Meoyoung"));

    }
}
