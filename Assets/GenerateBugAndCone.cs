using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBugAndCone : MonoBehaviour
{
    public float BugProbability;
    public GameObject bug;
    public GameObject cone;
    public static bool IsActive=true;
    private int stack;
    public GameObject Canvas;
    private float MaxGenerateDistance = 330f;//������ �� �ִ� �ִ�x��
    private void FixedUpdate()
    {
        if (!IsActive)
        {
            return;
        }
        stack++;
        if (stack >= 120)
        {
            stack = 0;
            //genertate
            float randomValue = Random.Range(0f, 100f);
            if (randomValue <= BugProbability) { 
                   //���� ����
                   var tmp=Instantiate(bug, Canvas.transform);
                tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0f, MaxGenerateDistance), 300f);
            }
            else
            {
                //�ֹ�� ����

                var tmp = Instantiate(cone, Canvas.transform);
                tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0f, MaxGenerateDistance), 300f);

            }

        }

    }
}
