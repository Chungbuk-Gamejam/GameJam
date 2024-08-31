using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBugAndCone : MonoBehaviour
{
    public float BugProbability = 20;
    public GameObject bug;
    public GameObject cone;
    public bool IsActive= false;
    private int stack = 0;
    public GameObject Canvas;
    private float MaxGenerateDistance = 330f;//생성될 수 있는 최대x값

    private void OnEnable()
    {
        IsActive = false;
        stack = 0;
        MaxGenerateDistance = 330f;
        BugProbability = 20;

        SoundManager.instance.TurnOffBackGroundMusic();
        SoundManager.instance.PlayBackgroundMusicForMiniGame();
    }

    private void OnDisable()
    {
        SoundManager.instance.TurnOffBackGroundMusic();
        SoundManager.instance.PlayBackgroundMusicForMainGame();
    }
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
                   //벌레 생성
                   var tmp=Instantiate(bug, Canvas.transform);
                tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0f, MaxGenerateDistance), 300f);
            }
            else
            {
                //솔방울 생성

                var tmp = Instantiate(cone, Canvas.transform);
                tmp.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(0f, MaxGenerateDistance), 300f);

            }

        }

    }
}
