using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarMove : MonoBehaviour
{
    public float speed ; // �̵� �ӵ�
    private int frameCount = 0; // ������ ī����
    private int direction = 1;  // �̵� ���� (1: ������, -1: ����)
    public int MaxFrameCount;
    public static bool hitChecker;
    public static bool IsAble;
    //public static bool IsStarted;
    public float darkenAmount = 0.2f;  // ��ο����� ������ ������ �� �ִ� ����
    private Color originalColor;       // ���� ���� ���� ���� ����
    private Color darkenedColor;       // ���� ������ �������� ���� �� ��ο� ��
    private bool isOriginal = true;    // ���� ������ ���� �������� ����
    private RectTransform myRectTransform;
    public GameObject targetObject;
    private void Awake()
    {
        hitChecker = false;
        IsAble = true;
        //IsStarted = false;
        myRectTransform=gameObject.GetComponent<RectTransform>();
    }
    void Start()
    {
        // ������ ����Ʈ�� 60���� ����
       
        // Renderer ������Ʈ�� ������
        darkenedColor = DarkenColor(originalColor);  // ���� ������ �������� ��ο� ���� ���
        UpdateColor();
    }


    // ���� ���¿� ���� ������ �����ϴ� �Լ�
    private void UpdateColor()
    {
        Color currentColor = isOriginal ? originalColor : darkenedColor;
    }

    // ������ ��Ӱ� �����ϴ� �Լ�
    private Color DarkenColor(Color color)
    {
        return new Color(
            color.r,
            color.g,
            color.b,
            color.a + 0.5f
            );
    }
    public static IEnumerator StopBar(float delaySeconds)
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
            isOriginal = true;
            UpdateColor();
        }
        else
        {
            isOriginal = false;
            UpdateColor();
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
