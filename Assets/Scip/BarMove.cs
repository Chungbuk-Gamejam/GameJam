using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarMove : MonoBehaviour
{
    public float speed = 8; // 이동 속도
    private int frameCount = 0; // 프레임 카운터
    private int direction = 1;  // 이동 방향 (1: 오른쪽, -1: 왼쪽)
    public int MaxFrameCount =32;
    public static bool hitChecker;
    public bool IsAble;
    private RectTransform myRectTransform;
    public GameObject targetObject;

    public Animator[] _stampAnims;

    private Vector2 initTransform;
    private void Awake()
    {
        hitChecker = false;
        //IsAble = true;
        //IsStarted = false;
        myRectTransform=gameObject.GetComponent<RectTransform>();
    }
    void Start()
    {
        initTransform = myRectTransform.anchoredPosition;
        // 프레임 레이트를 60으로 설정
        // Renderer 컴포넌트를 가져옴
    }
    private void OnEnable()
    {
        for(int i = 0; i< _stampAnims.Length; ++i)
        {
            _stampAnims[i].gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        SoundManager.instance.TurnOffBackGroundMusic();
        SoundManager.instance.PlayBackgroundMusicForMainGame();
    }

    public void Init()
    {
        IsAble = false;
        speed = 8;
        direction = 1;
        frameCount = 0;
        MaxFrameCount = 32;
        myRectTransform.anchoredPosition = initTransform;
    }




    public IEnumerator StopBar(float delaySeconds)
    {

        IsAble = false;  // 초기에 false로 설정
        yield return new WaitForSeconds(delaySeconds);  // 지정된 시간만큼 대기
        IsAble = true;  // n초 후에 true로 변경
    }


    void FixedUpdate()
    {
        if (IsAble)
        {
            myRectTransform.anchoredPosition=new Vector2(myRectTransform.anchoredPosition.x + direction * speed, myRectTransform.anchoredPosition.y);

            // 프레임 카운트 증가
            frameCount++;
        }
        // 매 프레임마다 위치 업데이트

        // 120 프레임마다 방향 전환
        if (frameCount == MaxFrameCount)
        { 
            direction *= -1; // 방향 전환
            frameCount = 0;  // 프레임 카운터 초기화
        }

    }
    private void Update()
    {
        if (IsAble)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                speed = 0;
                GaugeMove.Power = (Mathf.Abs((float)(frameCount - (MaxFrameCount / 2))) / (float)(MaxFrameCount / 2)) * 2 + 1;
                GaugeMove.Active = true;
                if (targetObject != null)
                {
                    targetObject.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("Target Object is not assigned.");
                }
                FishingBackground.IsActive = true;
            }
        }
    }
}
