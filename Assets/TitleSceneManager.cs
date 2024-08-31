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
        // 버튼을 클릭하면 코루틴을 시작합니다.
        StartCoroutine(TitleStart());
    }
    public IEnumerator TitleStart()
    {
        //할일 하고 
        Twinkling.ChangeTwinkling();
        yield return new WaitForSeconds(3f);
        StartCoroutine(SceneLoader.GetInstance().AsyncSceneLoader($"Intro"));

        // Debug.Log("10초 뒤인가요?"); 안됨.
        // StratScene("Start");
    }
    void Start()
    {
        // 버튼에 클릭 이벤트 리스너를 추가합니다.
        //myButton.onClick.AddListener(OnButtonClick);
    }
}
