using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    public Button myButton;
    

    public void OnButtonClick()
    {
        // ��ư�� Ŭ���ϸ� �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(TitleStart());
    }
    public IEnumerator TitleStart()
    {
        //���� �ϰ� 
        Twinkling.ChangeTwinkling();
        yield return new WaitForSeconds(3f);
        StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"Intro"));

        // Debug.Log("10�� ���ΰ���?"); �ȵ�.
        // StratScene("Start");
    }
    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ �����ʸ� �߰��մϴ�.
        //myButton.onClick.AddListener(OnButtonClick);
    }
}
