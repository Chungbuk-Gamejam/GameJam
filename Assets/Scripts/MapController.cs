using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    [Header("맵")]
    [SerializeField] GameObject map;
    [SerializeField] List<TMP_Text> region;
    [SerializeField] int counter = 0;
    [SerializeField] PlayerController playerController;
    [SerializeField] private List<GameObject> fadeObject;
    [SerializeField] private List<GameObject> lockObject;


    [Header("청화")]
    [SerializeField] List<Sprite> sprites;
    [SerializeField] List<Sprite> characterSprites;
    [SerializeField] Image character;
    [SerializeField] Image chunghwaImage;
    [SerializeField] GameObject chunghwa;

    private bool isActivated = false;

    private void OnEnable()
    {
        isActivated = false;
        counter = 0;
    }
    private void Update()
    {
        switch (counter)
        {
            case 0:
                SetFontSize(0);
                break;
            case 1:
                SetFontSize(1);
                break;
            case 2:
                SetFontSize(2);
                break;
            case 3:
                SetFontSize(3);
                break;
        }

        switch (playerController.dayCount)
        {
            case 1:
                fadeObject[0].SetActive(true);
                fadeObject[1].SetActive(true);
                fadeObject[2].SetActive(true);
                lockObject[0].SetActive(true);
                lockObject[1].SetActive(true);
                lockObject[2].SetActive(true);
                break;
            case 2:
                fadeObject[0].SetActive(false);
                fadeObject[1].SetActive(true);
                fadeObject[2].SetActive(true);
                lockObject[0].SetActive(false);
                lockObject[1].SetActive(true);
                lockObject[2].SetActive(true);
                break;
            case 3:
                fadeObject[0].SetActive(false);
                fadeObject[1].SetActive(false);
                fadeObject[2].SetActive(true);
                lockObject[0].SetActive(false);
                lockObject[1].SetActive(false);
                lockObject[2].SetActive(true);
                break;
            case 4:
                break;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (counter >= region.Count - 1)
            {
                counter = 0;
            }
            else
            {
                counter++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (counter == 0)
            {
                counter = region.Count - 1;
            }
            else
            {
                counter--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isActivated)
            {

                if (counter < playerController.dayCount)
                {
                    isActivated = true;
                    playerController.dayCount = counter + 1;
                    StartCoroutine(StartChangeScene());
                }
            }
        }

    }

    IEnumerator StartChangeScene()
    {
        yield return new WaitForSeconds(1.0f);
        FadeManager.Instance.StartFadeIn();
        yield return new WaitForSeconds(1.5f);

        

        if (playerController.cookCount == 1)
        {
            character.sprite = characterSprites[0];
        }
        else if (playerController.cookCount == 2)
        {
            character.sprite = characterSprites[1];
        }
        else if (playerController.cookCount == 3)
        {
            character.sprite = characterSprites[2];
        }

        chunghwaImage.sprite = sprites[counter];

        //Bgm
        SoundManager.instance.TurnOffBackGroundMusic();

        if (counter == 0)
        {
            SoundManager.instance.PlayBackgroundMusicForGoMilkyWay();
        }
        else if (counter == 1)
        {
            SoundManager.instance.PlayBackgroundMusicForGoAurora();
        }
        else if (counter == 2)
        {
            SoundManager.instance.PlayBackgroundMusicForGoConstellation();
        }
        else
        {
            SoundManager.instance.PlayBackgroundMusicForGoMeteorShower();
        }

        playerController.dayCount++;

        chunghwa.SetActive(true);
        FadeManager.Instance.StartFadeOut();

        yield return new WaitForSeconds(5.0f);
        FadeManager.Instance.StartFadeIn();
        yield return new WaitForSeconds(1.5f);
        playerController.gameObject.transform.position = playerController._transform.position;
        chunghwa.SetActive(false);
        FadeManager.Instance.StartFadeOut();
        playerController.Reset();
        playerController.ChangeState(playerController._idleState);

        SoundManager.instance.TurnOffBackGroundMusic();

        if (playerController.dayCount >= 4)
        {
            // 종료 로직
            StartCoroutine(SceneLoader.Instance.AsyncSceneLoader($"EndingCredit"));
            SoundManager.instance.PlayBackgroundMusicForGoMeteorShower();
        }
        else
        {
            SoundManager.instance.PlayBackgroundMusicForMainGame();
            map.SetActive(false);
        }
    }

    private void SetFontSize(int focus)
    {
        for (int i = 0; i < region.Count; i++)
        {
            if (i == focus)
            {
                region[i].fontSize = 42;
            }
            else
            {
                region[i].fontSize = 36;
            }
        }
    }

}
