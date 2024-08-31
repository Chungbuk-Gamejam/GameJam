using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class IntroScript2 : MonoBehaviour
{
    private string sentence="청화\n\n130년 주기로 일어나는 신비현상\n\n청화를 직접 본 사람은 마음 속 깊은 곳에 있는 염원 하나가 이뤄진다는 소문이 있다.\n\n나는 호기심에 이끌려 청화 현상을 보기 위해 이 곳에 캠핑을 왔다.v… … …v아 그건 그렇고 오늘은 뭐 해 먹지..?\n\n그냥 <size= 50> 꼬치구이 </size> 나 해먹어야겠다! 헤헤헤";
    private TextMeshProUGUI m_TextMeshProUGUI;
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
            yield return new WaitForSeconds(0.07f); //출력속도 유저가 저장할 수 있도록 / 배속 모드


        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"Meoyoung"));

    }
}
