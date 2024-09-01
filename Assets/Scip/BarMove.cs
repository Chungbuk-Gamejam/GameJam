using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarMove : MonoBehaviour
{
    public float speed = 8; // �̵� �ӵ�
    private int frameCount = 0; // ������ ī����
    private int direction = 1;  // �̵� ���� (1: ������, -1: ����)
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
        // ������ ����Ʈ�� 60���� ����
        // Renderer ������Ʈ�� ������
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

        IsAble = false;  // �ʱ⿡ false�� ����
        yield return new WaitForSeconds(delaySeconds);  // ������ �ð���ŭ ���
        IsAble = true;  // n�� �Ŀ� true�� ����
    }


    void FixedUpdate()
    {
        if (IsAble)
        {
            myRectTransform.anchoredPosition=new Vector2(myRectTransform.anchoredPosition.x + direction * speed, myRectTransform.anchoredPosition.y);

            // ������ ī��Ʈ ����
            frameCount++;
        }
        // �� �����Ӹ��� ��ġ ������Ʈ

        // 120 �����Ӹ��� ���� ��ȯ
        if (frameCount == MaxFrameCount)
        { 
            direction *= -1; // ���� ��ȯ
            frameCount = 0;  // ������ ī���� �ʱ�ȭ
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
