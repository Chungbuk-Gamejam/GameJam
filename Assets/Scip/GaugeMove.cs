using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static ItemInfo;

public class GaugeMove : MonoBehaviour
{

    public RectTransform uiElement; // UI 요소의 RectTransform을 참조합니다.
    public RectTransform BarRight;

    public static float Power=1;
    public float weight = 2f;
    public static bool Active=false;
    private float initHeight; //실제로는 width 값 (회전했으므로)
    private float initYPos; // 실제로는 x 값(22)
    public float clickPower = 40;

    [SerializeField] PlayerController playerController;
    [SerializeField] GetObject getObject;
    [SerializeField] BarMove barMove;
    [SerializeField] private GameObject fishGame;

    void Init()
    {
        clickPower = 40;
        weight = 2;
        Power = 1;
        uiElement.anchoredPosition = new Vector2(initYPos, uiElement.anchoredPosition.y);
        uiElement.sizeDelta = new Vector2(initHeight, uiElement.sizeDelta.y);
        BarRight.anchoredPosition = new Vector2(initYPos, BarRight.anchoredPosition.y);
        BarRight.sizeDelta = new Vector2(initHeight, BarRight.sizeDelta.y);
    }

    void DecreseNpx(float N)
    {
        N*=Time.deltaTime*40;
        uiElement.sizeDelta= new Vector2(uiElement.sizeDelta.x-N,uiElement.sizeDelta.y);
        uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x - N / 2, uiElement.anchoredPosition.y );
        BarRight.anchoredPosition=new Vector2(BarRight.anchoredPosition.x-N, BarRight.anchoredPosition.y );
    }

    private void Awake()
    {
        initHeight = uiElement.sizeDelta.x;
        initYPos= uiElement.anchoredPosition.x;
       if(fishGame == null)
        {
            fishGame = GameObject.FindWithTag("FishGame");
        }
    }
    

    void Update()
    {
        if (!Active) {
            return;
        }
        DecreseNpx(Power* weight);

        if (Input.GetMouseButtonUp(0))
        {
            uiElement.sizeDelta = new Vector2(uiElement.sizeDelta.x+ clickPower, uiElement.sizeDelta.y );
            uiElement.anchoredPosition = new Vector2(uiElement.anchoredPosition.x + clickPower / 2, uiElement.anchoredPosition.y);
            BarRight.anchoredPosition = new Vector2(BarRight.anchoredPosition.x + clickPower , BarRight.anchoredPosition.y);

        }
        if (uiElement.sizeDelta.x >= initHeight * 2)
        {
            Active = false;
            uiElement.sizeDelta = new Vector2(initHeight*2,uiElement.sizeDelta.y);
            uiElement.anchoredPosition = new Vector2(initYPos + initHeight / 2f,uiElement.anchoredPosition.y);
            BarRight.sizeDelta=new Vector2(0,0);
            FishingBackground.IsActive = false;

            //성공 로직
            getObject.AddSelectedItem(Item.Fish);
            StartCoroutine(StartFadeOut());
        }

        else if(uiElement.sizeDelta.x < 0.1f){
            Active = false;
            uiElement.sizeDelta = new Vector2(0f, uiElement.sizeDelta.y);
            BarRight.sizeDelta = new Vector2(0, 0);
            FishingBackground.IsActive = false;
            //실패로직
            StartCoroutine(StartFadeOut());
        }
    }

    IEnumerator StartFadeOut()
    {
        yield return new WaitForSeconds(3.0f);
        barMove.Init();
        Init();
        getObject.RotateClock();
        fishGame.SetActive(false);
        playerController.ChangeState(playerController._idleState);
    }
}
