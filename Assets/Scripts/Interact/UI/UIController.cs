using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [Header("인벤토리")]
    [Tooltip("인벤토리")]
    [SerializeField] public GameObject inventory;
    [Tooltip("플레이어 상태를 알기위한 컨트롤러")]
    [SerializeField] private PlayerController playerController;

    [Header("가마솥 UI")]
    [SerializeField] GameObject cookingUI;
    [SerializeField] GameObject cookingResultUI;
    [SerializeField] List<GameObject> image;
    [SerializeField] Animator cookAnimator;
    private WaitForSeconds gap = new WaitForSeconds(0.4f);
    private bool isActivated = false;


    public void ControllInventory()
    {
        if (playerController.CurrentState != playerController._waitState)
        {
            if (!inventory.activeSelf)
            {
                inventory.SetActive(true);
            }
            else
            {
                inventory.SetActive(false);
            }
        }
    }

    public void ControllCooking()
    {
        if (!isActivated)
        {
            if (!cookingUI.activeSelf)
            {
                cookingResultUI.SetActive(false);
                cookingUI.SetActive(true);
                StartCoroutine(StartAnimation());
            }
            else
            {
                cookingUI.SetActive(false);
                isActivated = true;
            }
        }
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        for(int i =0; i < image.Count; i++)
        {
            image[i].gameObject.SetActive(true);
            yield return gap;
        }
        yield return new WaitForSeconds(1.0f);
        cookAnimator.SetBool("Off", true);
    }
}
