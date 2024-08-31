using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] List<TMP_Text> region;
    [SerializeField] int counter = 0;
    [SerializeField] PlayerController playerController;

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

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(counter >= region.Count-1)
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
                counter = region.Count-1;
            }
            else
            {
                counter--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.dayCount = counter + 1;
        }
    }

    IEnumerator StartChangeScene()
    {
        yield return new WaitForSeconds(1.0f);
        FadeManager.Instance.StartFadeIn();
    }

    private void SetFontSize(int focus)
    {
        for (int i = 0; i < region.Count; i++)
        {
            if(i == focus)
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
