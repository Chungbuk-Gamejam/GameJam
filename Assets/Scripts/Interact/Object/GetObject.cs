using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ItemInfo;

public class GetObject : MonoBehaviour, IInteractable
{
    public enum ItemType
    {
        Vegetable,
        Meat,
        Fish,
        Spice,
        Pinecone
    }
    [Tooltip("아이템을 추가할 인벤토리")]
    [SerializeField] private InventoryManager inventoryManager;
    [Tooltip("아이템 이름")]
    [SerializeField] private ItemType itemTypeInfo;
    [Tooltip("회전 시킬 오브젝트")]
    [SerializeField] private RectTransform rectTransform;
    [Tooltip("카운터를 적용할 플레이어 컨트롤러")]
    [SerializeField] private PlayerController playerController;
    [Tooltip("미니게임 컨트롤러")]
    [SerializeField] UIController _uiController;

    [Header("미니게임1 (생선)")]
    [SerializeField] private GameObject fishGame;
    [SerializeField] private BarMove barMove;

    [Header("미니게임2 (솔방울)")]
    [SerializeField] private GameObject pineGame;
    [SerializeField] private GenerateBugAndCone generateBugAndCone;
    [SerializeField] private PineconeMove pineconeMove;

    [Header("미니게임3 (고기)")]
    [SerializeField] private GameObject meatGame;
    [SerializeField] private MiniGameObject2 miniGameObject2;

    [Header("미니게임4 (향신료)")]
    [SerializeField] private GameObject spiceGame;
    [SerializeField] private MiniGameObject3 miniGameObject3;




    private void Start()
    {
        if (inventoryManager == null)
        {
            inventoryManager = FindFirstObjectByType<InventoryManager>();
        }

        if (rectTransform == null)
        {
            rectTransform = GameObject.FindWithTag("Clock").GetComponent<RectTransform>();
        }

        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }

        if (_uiController == null)
        {
            _uiController = FindFirstObjectByType<UIController>();
        }

        if(fishGame == null)
        {
            fishGame = GameObject.FindWithTag("FishGame");
        }

        
    }
    //인벤토리에 아이템을 추가한 후 필드에서 아이템 삭제
    //상호작용 카운터 1 감소
    public void Interact()
    {
        playerController.ChangeState(playerController._waitState);
        CheckGameType();
        //AddItem();
        //Destroy(gameObject);
    }

    public void CheckGameType()
    {
        switch (itemTypeInfo)
        {
            case ItemType.Fish:
                fishGame.SetActive(true);
                StartCoroutine(StartFishGame());
                break;
            case ItemType.Pinecone:
                pineGame.SetActive(true);
                StartCoroutine(StartPineGame());
                break;
            case ItemType.Meat:
                meatGame.SetActive(true);
                StartCoroutine(StartMeatGame());
                break;
            case ItemType.Spice:
                spiceGame.SetActive(true);
                StartCoroutine(StartSpiceGame());
                break;

        }
    }

    private IEnumerator StartSpiceGame()
    {
        yield return new WaitForSeconds(3.0f);
        miniGameObject3.SetCompleteCallback((x) =>
        {
            if (x == true)
            {
                int randNum = Random.Range(0, 2);
                switch (randNum)
                {
                    case 0:
                        AddSelectedItem(Item.ChiliPepper);
                        break;
                    case 1:
                        AddSelectedItem(Item.Soy);
                        break;
                }
                spiceGame.SetActive(false);
            }
        });
        miniGameObject3.ChangeState();
    }

    private IEnumerator StartMeatGame()
    {
        yield return new WaitForSeconds(3.0f);
        miniGameObject2.SetCompleteCallback((x) =>
        {
            if (x == true)
            {
                AddSelectedItem(Item.Meat);
                meatGame.SetActive(false);
            }
        });
        miniGameObject2.ChangeState();
    }


    private IEnumerator StartPineGame()
    {
        yield return new WaitForSeconds(3.0f);
        pineconeMove.IsActive = true;
        generateBugAndCone.IsActive = true;
    }
    private IEnumerator StartFishGame()
    {
        yield return new WaitForSeconds(3.0f);
        barMove.IsAble = true;
    }

    public void AddSelectedItem(Item item)
    {
        inventoryManager.AddItem(item);
    }

    public void AddItem()
    {

        switch (itemTypeInfo)
        {
            case ItemType.Vegetable:
                switch (Random.Range(0, 7))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Garlic);
                        break;
                    case 1:
                        inventoryManager.AddItem(Item.GreenOnion);
                        break;
                    case 2:
                        inventoryManager.AddItem(Item.Mushroom);
                        break;
                    case 3:
                        inventoryManager.AddItem(Item.Potato);
                        break;
                    case 4:
                        inventoryManager.AddItem(Item.Radish);
                        break;
                }
                break;
            case ItemType.Spice:
                switch (Random.Range(0, 2))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Soy);
                        break;
                    case 1:
                        inventoryManager.AddItem(Item.ChiliPepper);
                        break;
                }
                break;
            case ItemType.Meat:
                switch (Random.Range(0, 1))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Meat);
                        break;
                }
                break;
            case ItemType.Fish:
                switch (Random.Range(0, 1))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Fish);
                        break;
                }
                break;
            case ItemType.Pinecone:
                switch (Random.Range(0, 1))
                {
                    case 0:
                        inventoryManager.AddItem(Item.Pinecone);
                        break;
                }
                break;
        }

    }

    public void RotateClock()
    {
        playerController.interactCounter--;

        rectTransform.localEulerAngles = new Vector3(
                rectTransform.localEulerAngles.x,
                rectTransform.localEulerAngles.y,
                rectTransform.localEulerAngles.z - 15f
            );
    }
}
