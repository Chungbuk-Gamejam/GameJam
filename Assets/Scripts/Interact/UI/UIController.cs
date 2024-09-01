using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("인벤토리")]
    [Tooltip("인벤토리")]
    [SerializeField] public GameObject inventory;
    [Tooltip("플레이어 상태를 알기위한 컨트롤러")]
    [SerializeField] private PlayerController playerController;
    [Tooltip("인벤토리 매니저")]
    [SerializeField] private InventoryManager inventoryManager;

    [Header("가마솥 UI")]
    [SerializeField] GameObject cookingUI;
    [SerializeField] GameObject cookingResultUI;
    [SerializeField] List<GameObject> image;
    [SerializeField] Animator cookAnimator;
    [SerializeField] Transform _cookCover;
    private WaitForSeconds gap = new WaitForSeconds(0.4f);
    private bool isActivated = false;

    [Header("메인 게임")]
    [SerializeField] private GameObject mainGame;
    [SerializeField] private MiniGameObject miniGameObject;
    [SerializeField] private GameObject illust;
    [SerializeField] private GameObject cookBtn;
    [SerializeField] private GameObject campBtn;
    [SerializeField] private Image IllustPanel;
    [SerializeField] private Sprite failIllust;
    [SerializeField] private Sprite successIllust;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject telescope;
    [SerializeField] private GameObject map;

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
            if (cookingResultUI.activeSelf)
            {
                cookingResultUI.SetActive(false);
                cookingUI.SetActive(true);
                StartCoroutine(StartAnimation());
            }
            else
            {
                cookingUI.SetActive(false);
                _cookCover.localPosition = new Vector3(0f, 167f, 0f); 
                isActivated = true;
            }
        }
    }

    public void ControllerCamp()
    {
        cookBtn.SetActive(true);
        campBtn.SetActive(false);
        cookingResultUI.SetActive(false);
        isActivated = false;
        FadeManager.Instance.StartFadeIn();
        StartCoroutine(StartChangeState());
    }

    IEnumerator StartChangeState()
    {
        yield return new WaitForSeconds(2.0f);
        playerController.ChangeState(playerController._waitState);
        character.SetActive(true);
        telescope.SetActive(true);

        yield return new WaitForSeconds(6.0f);
        character.SetActive(false); 
        telescope.SetActive(false);
        FadeManager.Instance.StartFadeOut();
        map.SetActive(true);
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < image.Count; i++)
        {
            image[i].gameObject.SetActive(true);
            yield return gap;
        }
        yield return new WaitForSeconds(1.0f);
        cookAnimator.SetBool("Off", true);

        yield return new WaitForSeconds(3.0f);

        miniGameObject.SetCompleteCallback((x) =>
        {
            if (x == false)
            {
                playerController.cookCount--;
                
            }

            if(playerController.cookCount < 2)
            {
                IllustPanel.sprite = failIllust;
            }
            else
            {
                IllustPanel.sprite = successIllust;
            }

            StartCoroutine(ShowRecipeSlot());
        });
        mainGame.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        cookAnimator.SetBool("Off", true);
        for (int i = 0; i < image.Count; i++)
        {
            image[i].gameObject.SetActive(false);
            yield return null;
        }
        miniGameObject.isActivated = true;
    }

    IEnumerator ShowRecipeSlot()
    {
        yield return new WaitForSeconds(2.0f);
        miniGameObject.isActivated = false;
        cookingUI.SetActive(false);
        _cookCover.localPosition = new Vector3(0f, 167f, 0f);
        mainGame.SetActive(false);
        switch (playerController.dayCount)
        {
            case 1:
                inventoryManager.CreateRecipeSlots(RecipeManager.instance.skewers);
                break;
            case 2:
                inventoryManager.CreateRecipeSlots(RecipeManager.instance.steamedFish);
                break;
            case 3:
                inventoryManager.CreateRecipeSlots(RecipeManager.instance.mushroomSoup);
                break;
            case 4:
                inventoryManager.CreateRecipeSlots(RecipeManager.instance.jjaggle);
                break;
        }
        cookBtn.SetActive(false);
        inventoryManager.GetResultStamp();
        yield return new WaitForSeconds(1.0f);
        illust.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        campBtn.SetActive(true);
    }
}
